using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetApplicationList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BusinessName { get; set; }
        public string LogoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int JobId { get; set; }
    }
}
