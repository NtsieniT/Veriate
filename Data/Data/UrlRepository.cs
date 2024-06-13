using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class UrlRepository : IUrlRepository
    {
        private readonly DataContext _context;

        public UrlRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddUrlMappingAsync(UrlMapping urlMapping)
        {
            _context.UrlMappings.Add(urlMapping);
            await _context.SaveChangesAsync();
        }

        public async Task<UrlMapping> GetUrlMappingAsync(string alias)
        {
            return await _context.UrlMappings.FirstOrDefaultAsync(u => u.Alias == alias);
        }

        public async Task<List<UrlMapping>> GetExpiredUrlMappingsAsync()
        {
            return await _context.UrlMappings
                                 .Where(u => u.ExpirationTime.HasValue && u.ExpirationTime.Value < DateTime.UtcNow)
                                 .ToListAsync();
        }

        public async Task RemoveUrlMappingsAsync(List<UrlMapping> urlMappings)
        {
            _context.UrlMappings.RemoveRange(urlMappings);
            await _context.SaveChangesAsync();
        }

    }
}
