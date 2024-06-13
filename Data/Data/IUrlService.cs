using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public interface IUrlService
    {
        Task<UrlResponse> ShortenUrlAsync(string originalUrl, DateTime? expirationTime);
        Task<string> GetOriginalUrlAsync(string alias);
    }
}
