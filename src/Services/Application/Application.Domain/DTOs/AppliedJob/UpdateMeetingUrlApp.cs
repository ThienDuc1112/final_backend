using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class UpdateMeetingUrlApp
    {
        [Required(ErrorMessage = "Id cannot be empty !!")]
        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Meeting url cannot be empty !!")]
        public string Url { get; set; }
    }
}
