using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.BusinessInfor
{
    public class GetBusinessAdminDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public string IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
