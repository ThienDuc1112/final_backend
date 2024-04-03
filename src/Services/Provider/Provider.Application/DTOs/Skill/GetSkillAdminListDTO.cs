using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Skill
{
    public class GetSkillAdminListDTO
    {
        public List<GetSkillAdminDTO> GetSkillAdminDTOs { get; set; }
        public int TotalNumber { get; set; }
    }
}
