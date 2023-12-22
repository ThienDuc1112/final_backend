using Provider.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Entities
{
    public class Language : EntityBase
    {
        public string LanguageName { get; set; }
        public string Level { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
