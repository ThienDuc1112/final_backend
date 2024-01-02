using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [ForeignKey("BusinessId")]
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
