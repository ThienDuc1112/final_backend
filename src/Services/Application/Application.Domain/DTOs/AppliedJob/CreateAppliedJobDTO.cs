using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class CreateAppliedJobDTO
    {
        [Required(ErrorMessage = "CandidateId cannot be empty !!")]
        public string CandidateId { get; set; }

        [Required(ErrorMessage = "JobId cannot be empty !!")]
        [Range(1, long.MaxValue, ErrorMessage = "JobId must be greater than 0")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "ResumeId cannot be empty !!")]
        [Range(1, long.MaxValue, ErrorMessage = "ResumeId must be greater than 0")]
        public int ResumeId { get; set; }
    }
}
