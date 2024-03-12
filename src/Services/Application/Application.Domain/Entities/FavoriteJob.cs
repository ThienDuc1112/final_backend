using Application.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Entities
{
    public class FavoriteJob : EntityBase
    {
        public string CandidateId { get; set; }
        public int JobId { get; set; }
    }
}
