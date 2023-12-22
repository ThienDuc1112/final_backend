using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.DTOs.Language
{
    public class LanguageDTO
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string Level { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
