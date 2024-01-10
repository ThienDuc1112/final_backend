using Business.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProviderController : Controller
    {

        [HttpGet("GetBusinessSize")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BusinessSize>>> GetBusinessSize()
        {
            IEnumerable<BusinessSize> sizes = BusinessSize.SupportedSizes;
            return Ok(sizes);
        }


        [HttpGet("GetCareerLevel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CareerLevel>>> GetCareerLevel()
        {
            IEnumerable<CareerLevel> careerLevels = CareerLevel.SupportedLevels;
            return Ok(careerLevels);
        }

        [HttpGet("GetEducationLevel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<EducationLevel>>> GetEducationLevel()
        {
            IEnumerable<EducationLevel> educationLevels = EducationLevel.GetEducationLevels;
            return Ok(educationLevels);
        }

        [HttpGet("GetExperienceYear")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExperienceYear>>> GetExperienceYear()
        {
            IEnumerable<ExperienceYear> expYears = ExperienceYear.GetYearExperiences;
            return Ok(expYears);
        }

        [HttpGet("GetJobType")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<JobType>>> GetJobType()
        {
            IEnumerable<JobType> jobTypes = JobType.SupportedTypes;
            return Ok(jobTypes);
        }

    }
}
