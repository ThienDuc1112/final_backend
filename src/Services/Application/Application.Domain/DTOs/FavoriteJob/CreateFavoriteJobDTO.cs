using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.FavoriteJob
{
    public class CreateFavoriteJobDTO
    {
        public string CandidateId { get; set; }
        public int JobId { get; set; }
    }
}
