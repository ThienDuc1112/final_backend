using Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Contracts
{
    public interface IBusinessRepository : IGenericRepository<BusinessInfor>
    {
        Task AcceptOrReject(int businessId);
        Task GetBusinessInforWithRelevant(int businessId);
    }
}
