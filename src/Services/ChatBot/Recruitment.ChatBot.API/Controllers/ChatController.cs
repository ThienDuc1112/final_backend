using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Recruitment.ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChatController : Controller
    {
        [HttpGet("text")]
        public async Task<ActionResult<object>> GetResponse(string userMessage)
        {
            return Ok(new { message = "result" });
        }

        [HttpGet("{userMessage}")]
        public async Task<ActionResult<object>> GetMessage(string userMessage)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://job-assistant.openai.azure.com/openai/deployments/gpt-35-turbo-16k/extensions/chat/completions?api-version=2023-06-01-preview");
            request.Headers.Add("api-key", "ef25286ceaaa496895e401f4c8147741");
            var content = new StringContent($"{{\r\n\"dataSources\": [\r\n{{\r\n\"type\": \"AzureCognitiveSearch\",\r\n\"parameters\": {{\r\n\"endpoint\": \"https://searchjob.search.windows.net\",\r\n\"indexName\": \"searchjob-index\",\r\n\"semanticConfiguration\": \"searchbyrequirement-config\",\r\n\"queryType\": \"semantic\",\r\n\"fieldsMapping\": {{\r\n\"contentFieldsSeparator\": \"\\n\",\r\n\"contentFields\": [\r\n\"EducationLevelMin\",\r\n\"YearExpMin\",\r\n\"JobType\",\r\n\"CareerLevel\",\r\n\"Description\",\r\n\"Welfare\",\r\n\"Requirement\",\r\n\"RequiredSkills\",\r\n\"Responsibilities\"\r\n],\r\n\"filepathField\": \"Id\",\r\n\"titleField\": \"Title\",\r\n\"urlField\": null,\r\n\"vectorFields\": []\r\n}},\r\n\"inScope\": true,\r\n\"roleInformation\": \"You are an AI assistant that helps people find information.\",\r\n\"filter\": null,\r\n\"strictness\": 3,\r\n\"topNDocuments\": 5,\r\n\"key\": \"rKFsk3JoEcDIlZfBnF5j0CvjvpZ20w7aT4T3Xv7ZQuAzSeAWGC1m\"\r\n}}\r\n}}\r\n],\r\n\"messages\": [\r\n{{\r\n\"role\": \"system\",\r\n\"content\": \"You are an AI assistant that helps people find information.\"\r\n}},\r\n{{\r\n\"role\": \"user\",\r\n\"content\": \"{userMessage}\"\r\n}}\r\n],\r\n\"deployment\": \"gpt-35-turbo-16k\",\r\n\"temperature\": 0,\r\n\"top_p\": 1,\r\n\"max_tokens\": 800,\r\n\"stop\": null,\r\n\"stream\": false\r\n}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var resultObject = JObject.Parse(result);
            var res = (string)resultObject["choices"][0]["messages"][1]["content"];
            return Ok(new { message = res });
        }
    }
}
