using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Contracts
{
    public interface IUnitOfWork
    {
        IAreaRepository AreaRepository { get; }
        IBusinessRepository BusinessRepository { get; }
        IJobRepository JobRepository { get; }
        IMediaRepository MediaRepository { get; }

        Task Save();
    }
}
