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


        public async Task<BusinessInfor> GetBusinessInforWithRelevant(string Id)
        {
            var business = _dbContext.Businesses.Where(b => b.UserId == Id)
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
                    latitude = b.latitude,
                    longitude = b.longitude,
                    UserId = b.UserId,
                    CreatedDate = b.CreatedDate,
                    Areas = b.Areas.Select(a => new Area
                    {
                        Id = a.Id,
                        CareerId = a.CareerId,
                    }).ToList(),
                    Medias = b.Medias
                    .Select(p => new Media
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Type = p.Type,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            
            return await business;
        }
    }
}
