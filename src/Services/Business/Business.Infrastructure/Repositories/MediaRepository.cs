using Business.Application.Contracts;
using Business.Domain.Entities;
using Business.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure.Repositories
{
    public class MediaRepository : GenericRepository<Media>, IMediaRepository
    {
        private readonly BusinessDbContext _dbContext;
        public MediaRepository(BusinessDbContext businessDbContext) : base(businessDbContext)
        {
            _dbContext = businessDbContext;
        }
        public async Task<List<Media>> GetAllVideosByBusinessId(int id)
        {
            return await _dbContext.Medias.Where(m => m.BusinessId == id && m.Type == "video").ToListAsync();
        }

        public async Task<List<Media>> GetPicturesByBusinessId(int id)
        {
            return await _dbContext.Medias.Where(m => m.BusinessId == id && m.Type == "picture").ToListAsync();
        }
    }
}
