using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShorter.Services.Domain.Entities;

namespace UrlShorter.Services.Domain.Repositories
{
    public interface IUrlMappingRepository
    {
        Task<string> GetLongLinkAsync(string code);
        Task AddAsync(UrlMapping urlMapping);
        Task<UrlMapping> FindAndExtend(UrlMapping entity);
    }
}
