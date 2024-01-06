using Business.Application.Contracts;
using Business.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BusinessDbContext _dbContext;
        private IBusinessRepository _usinessRepository;
        private IMediaRepository _mediaRepository;
        private IAreaRepository _areaRepository;
        private IJobRepository _jobRepository;

        public UnitOfWork(BusinessDbContext businessDbContext)
        {
            _dbContext = businessDbContext;
        }
        public IAreaRepository AreaRepository =>
            _areaRepository ??= new AreaRepository(_dbContext);
        public IBusinessRepository BusinessRepository => 
            _usinessRepository ??= new BusinessRepository(_dbContext);
        public IJobRepository JobRepository => 
            _jobRepository ??= new JobRepository(_dbContext);
        public IMediaRepository MediaRepository =>
            _mediaRepository ??= new MediaRepository(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
