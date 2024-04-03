using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.Matching
{
    public class TextData
    {
        public string Text { get; set; }
    }

    public class TransformedData
    {
        public string[] Tokens { get; set; }
    }
}
