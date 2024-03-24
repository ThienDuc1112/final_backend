using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Skill
{
    public class GetSkillAdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CareerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
