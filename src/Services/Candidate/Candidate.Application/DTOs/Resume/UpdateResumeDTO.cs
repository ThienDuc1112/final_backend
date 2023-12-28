using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.DTOs.Resume
{
    public class UpdateResumeDTO
    {
        public int Id { get; set; }
        public int CareerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Linkedln { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StatusOfEmployment { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
