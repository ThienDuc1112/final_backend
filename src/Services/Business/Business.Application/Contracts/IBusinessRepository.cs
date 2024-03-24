using Business.Application.DTOs.BusinessInfor;
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
        Task AcceptOrReject(BusinessInfor businessInfor);
        Task<BusinessInfor> GetBusinessInforWithRelevant(string businessId);
        Task<BusinessInfor> GetBusinessDetail(int id);
        Task<BusinessInfor> GetBusinessID(string userId);

        Task<GetBusinessListAdminDTO> GetBusinessListAdmin(int page, string? status);
    }
}
