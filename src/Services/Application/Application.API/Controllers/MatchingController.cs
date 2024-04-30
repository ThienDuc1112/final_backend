using Application.API.GrpcServices;
using Application.Domain.DTOs.AppliedJob;
using Application.Domain.DTOs.Matching;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.ML;
using Candidate.Grpc.Protos;
using Business.Grpc.Protos;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MatchingController : Controller
    {
        private readonly MatchingResumeGrpcService _resumeGrpcService;
        private readonly MatchingJobGrpcService _jobGrpcService;

        public MatchingController(MatchingResumeGrpcService resumeGrpcService, MatchingJobGrpcService jobGrpcService)
        {
            _resumeGrpcService = resumeGrpcService;
            _jobGrpcService = jobGrpcService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MatchingList>> GetMatching([FromQuery(Name = "resumeId")] int resumeId, [FromQuery(Name = "jobId")] int jobId)
        {
            MatchingResumeModel resume = await _resumeGrpcService.GetMatchingResume(resumeId);
            MatchingJobModel job = await _jobGrpcService.GetMatchingJob(jobId);
            
            return Ok(CalculateMatching(resume, job));
        }

         

        private string RemoveStopword(string text)
        {
            var context = new MLContext();
            var emptyData = new List<TextData>();
            var data = context.Data.LoadFromEnumerable(emptyData);
            var tokenization = context.Transforms.Text.TokenizeIntoWords("Tokens", "Text",
                    separators: new[] { ' ', '.', ',', '•', '/','(',')',':',';' })
                .Append(context.Transforms.Text.RemoveDefaultStopWords("Tokens", "Tokens",
                    Microsoft.ML.Transforms.Text.StopWordsRemovingEstimator.Language.English));

            var model = tokenization.Fit(data);
            var engine = context.Model.CreatePredictionEngine<TextData, TransformedData>(model);

            var emptyInput = new TextData();
            var transformedData = engine.Predict(new TextData { Text = text});

            var output = string.Join(" ", transformedData.Tokens.Where(word => char.IsUpper(word.FirstOrDefault())));

            return output;
        }

        private List<string> GetWords(string text)
        {
            var separators = new[] { ' ', '.', ',', ';', ':', '!', '?' };
            var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(words);
        }

        private Dictionary<string, int> GetWordFrequencies(List<string> words)
        {
            var wordFrequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequencies.ContainsKey(word))
                    wordFrequencies[word]++;
                else
                    wordFrequencies[word] = 1;
            }
            return wordFrequencies;
        }

        private double CalculateDotProduct(Dictionary<string, int> wordFrequency1, Dictionary<string, int> wordFrequency2)
        {
            var commonWords = wordFrequency1.Keys.Intersect(wordFrequency2.Keys);
            double dotProduct = commonWords.Sum(word => wordFrequency1[word] * wordFrequency2[word]);
            return dotProduct;
        }

        private double CalculateMagnitude(Dictionary<string, int> wordFrequency)
        {
            double magnitude = Math.Sqrt(wordFrequency.Values.Sum(frequency => frequency * frequency));
            return magnitude;
        }

        private double CalculateCosineSimilarity(string text1, string text2)
        {
            var words1 = GetWords(text1);
            var words2 = GetWords(text2);

            var wordFrequency1 = GetWordFrequencies(words1);
            var wordFrequency2 = GetWordFrequencies(words2);

            var dotProduct = CalculateDotProduct(wordFrequency1, wordFrequency2);
            var magnitude1 = CalculateMagnitude(wordFrequency1);
            var magnitude2 = CalculateMagnitude(wordFrequency2);

            double cosineSimilarity = dotProduct / (magnitude1 * magnitude2);
            return Math.Round(cosineSimilarity, 2);
        }

        private int GetExperienceYear(string year)
        {
            if (year == "Less than one year")
            {
                return 0;
            }
            else if (year == "One to three years")
            {
                return 1;
            }
            else if (year == "Three to five years")
            {
                return 3;
            }
            else if (year == "Five to ten years")
            {
                return 5;
            }
            else
            {
                return 10;
            }
        }

        private MatchingList CalculateMatching(MatchingResumeModel resume, MatchingJobModel job)
        {
            var matchingList = new MatchingList();
            double score = 0;
            if(resume.CareerId == job.CareerId)
            {
                score += 25;
                var item = new MatchingItem() { IsMatch = true, Name = "Candidate is working in the same industry as the job" };
                matchingList.MatchingItems.Add(item);
            }
            else
            {
                var item = new MatchingItem() { IsMatch = false, Name = "Candidate doesn't meet the career type requirement" };
                matchingList.MatchingItems.Add(item);
            }
            if(resume.EducationDegree.Any(d => d.Equals(job.EducationLevelMin)))
            {
                score += 15;
                var item = new MatchingItem() { IsMatch = true, Name = $"Candidate meets education degree requirement" };
                matchingList.MatchingItems.Add(item);
            }
            else
            {
                var item = new MatchingItem() { IsMatch = false, Name = "Candidate doesn't meet the education degree requirement" };
                matchingList.MatchingItems.Add(item);
            }
            int yearRequirement = GetExperienceYear(job.YearExpMin);
            if(resume.ExperienceYear >= yearRequirement)
            {
                score += 20;
                var item = new MatchingItem() { IsMatch = true, Name = "Candidate meets working experience requirement" };
                matchingList.MatchingItems.Add(item);
            }
            else
            {
                var item = new MatchingItem() { IsMatch = false, Name = $"Candidate doesn't meet the working experience requirement" };
                matchingList.MatchingItems.Add(item);
            }
            if(resume.Gender.ToLower() == job.GenderRequirement.ToLower() || job.GenderRequirement == "N/A")
            {
                score += 5;
                var item = new MatchingItem() { IsMatch = true, Name = "Candidate meets the gender requirement" };
                matchingList.MatchingItems.Add(item);
            }
            else
            {
                var item = new MatchingItem() { IsMatch = false, Name = $"Candidate doesn't meet the gender requirement" };
                matchingList.MatchingItems.Add(item);
            }

            if(resume.Languages.Any(l => l.Equals(job.LanguageRequirementId)))
            {
                score += 10;
                var item = new MatchingItem() { IsMatch = true, Name = "Candidate meets the language requirement" };
                matchingList.MatchingItems.Add(item);
            }
            else
            {
                var item = new MatchingItem() { IsMatch = false, Name = $"Candidate doesn't meet the language requirement" };
                matchingList.MatchingItems.Add(item);
            }

            var resumeSkills = string.Join(" ", resume.Skills) + " " + resume.AdditionalSkill;
            var jobSkills = RemoveStopword(job.RequiredSkills);
            double similarity = CalculateCosineSimilarity(resumeSkills, jobSkills);

            score = score + (similarity * 25);
            var itemSkill = new MatchingItem() { IsMatch = true, Name = $"Matching rate of skills between job and candidate: {similarity}%" };
            matchingList.MatchingItems.Add(itemSkill);
            matchingList.Score = score;

            return matchingList;
        }
    }
}
