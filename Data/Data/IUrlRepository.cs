using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public interface IUrlRepository
    {
        Task AddUrlMappingAsync(UrlMapping urlMapping);
        Task<UrlMapping> GetUrlMappingAsync(string alias);
        Task<List<UrlMapping>> GetExpiredUrlMappingsAsync();
        Task RemoveUrlMappingsAsync(List<UrlMapping> urlMappings);
    }
}
