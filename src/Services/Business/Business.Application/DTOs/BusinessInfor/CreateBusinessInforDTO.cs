using Business.Application.DTOs.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.BusinessInfor
{
    public class CreateBusinessInforDTO
    {
        public string FullName { get; set; }
        public int FoundedYear { get; set; }
        public string BusinessSize { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseFont { get; set; }
        public string LicenseBack { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }

        public List<CreateAreaDTO> AreaDTOs { get; set; }

    }
}
