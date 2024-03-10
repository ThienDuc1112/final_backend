using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Career
{
    public class GetCareerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetSkillDTO> Skills { get; set; }
    }
}
