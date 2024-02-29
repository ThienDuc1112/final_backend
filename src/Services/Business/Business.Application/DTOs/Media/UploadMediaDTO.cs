using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Media
{
    public class UploadMediaDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int BusinessId { get; set; }
    }
}
