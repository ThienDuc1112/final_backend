using Provider.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Entities
{
    public class Career : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAllowed { get; set; }

    }
}
