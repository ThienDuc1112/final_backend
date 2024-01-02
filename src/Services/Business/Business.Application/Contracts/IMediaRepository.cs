using Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Contracts
{
    public interface IMediaRepository : IGenericRepository<Media>
    {
        Task<List<Media>> GetPicturesByBusinessId(int id);
        Task<List<Media>> GetAllVideosByBusinessId(int id);
    }
}
