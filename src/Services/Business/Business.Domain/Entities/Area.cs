using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public int CareerId { get; set; }
        [ForeignKey("BusinessId")]
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
