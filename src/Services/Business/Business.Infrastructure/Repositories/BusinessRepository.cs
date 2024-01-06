using Business.Application.Contracts;
using Business.Application.Exceptions;
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
    public class BusinessRepository : GenericRepository<BusinessInfor>, IBusinessRepository
    {
        private readonly BusinessDbContext _dbContext;
        public BusinessRepository(BusinessDbContext businessDbContext) : base(businessDbContext)
        {
            _dbContext = businessDbContext;
        }
        public async Task AcceptOrReject(BusinessInfor businessInfor)
        {
           
            _dbContext.Entry(businessInfor).State = EntityState.Modified;
        }


        public async Task GetBusinessInforWithRelevant(int businessId)
        {
            var business = _dbContext.Businesses.Where(b => b.Id == businessId)
                .Select(b => new BusinessInfor
                {
                    Id = b.Id,
                    FullName = b.FullName,
                    ShortName = b.ShortName,
                    BusinessSize = b.BusinessSize,
                    PhoneNumber = b.PhoneNumber,
                    Address = b.Address,
                    Email = b.Email,
                    Description = b.Description,
                    FaceBookUrl = b.FaceBookUrl,
                    WebsiteUrl = b.WebsiteUrl,
                    LogoUrl = b.LogoUrl,
                    LinkedInUrl = b.LinkedInUrl,       
                    FoundedYear = b.FoundedYear,
                    LicenseBack = b.LicenseBack,
                    LicenseFont = b.LicenseFont,     
                    TaxCode = b.TaxCode,
                    Areas = b.Areas.Select(a => new Area
                    {
                        Id = a.Id,
                        CareerId = a.CareerId,
                    }).ToList(),
                    Pictures = b.Pictures.Where(b => b.Type == "picture")
                    .Select(p => new Media{
                        Id = p.Id,
                        Name = p.Name,
                    }).ToList(),
                    Videos = b.Videos.Where(b => b.Type == "video")
                    .Select(p => new Media
                    {
                        Id = p.Id,
                        Name = p.Name,
                    }).ToList(),
                })
            ;
            
        }
    }
}
