using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Skill
{
    public class SkillDTO
    {
        public int Id { get; set; }
        public string NameSkill { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public CareerDTO CareerDTO { get; set; }
    }
}
