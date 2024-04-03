using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.Matching
{
    public class MatchingList
    {
        public List<MatchingItem> MatchingItems { get; set; } = new List<MatchingItem>();
        public double Score { get; set; }
    }
}
