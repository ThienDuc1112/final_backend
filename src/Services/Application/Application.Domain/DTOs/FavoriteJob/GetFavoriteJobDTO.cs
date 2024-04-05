using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.FavoriteJob
{
    public class GetFavoriteJobDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int BusinessId { get; set; }
        public string JobName { get; set; }
        public string LogoUrl { get; set; }
        public string BusinessName { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}
