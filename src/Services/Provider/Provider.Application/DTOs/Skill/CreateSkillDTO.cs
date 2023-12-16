using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Skill
{
    public class CreateSkillDTO
    {
        public string NameSkill { get; set; }
        public int CareerId { get; set; }
    }
}
