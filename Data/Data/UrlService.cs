using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IAliasGenerator _aliasGenerator;

        public UrlService(IUrlRepository urlRepository, IAliasGenerator aliasGenerator)
        {
            _urlRepository = urlRepository;
            _aliasGenerator = aliasGenerator;
        }

        public async Task<UrlResponse> ShortenUrlAsync(string originalUrl, DateTime? expirationTime)
        {
            var alias = _aliasGenerator.GenerateAlias();
            var urlMapping = new UrlMapping
            {
                OriginalUrl = originalUrl,
                Alias = alias,
                ExpirationTime = expirationTime
            };
            await _urlRepository.AddUrlMappingAsync(urlMapping);

            return new UrlResponse()
            {
                ShortUrl = $"{alias}"
            };
            //return $"http://short.ly/{alias}";
        }

        public async Task<string> GetOriginalUrlAsync(string alias)
        {
            var urlMapping = await _urlRepository.GetUrlMappingAsync(alias);
            if (urlMapping == null || (urlMapping.ExpirationTime.HasValue && urlMapping.ExpirationTime.Value < DateTime.UtcNow))
            {
                return null;
            }
            return urlMapping.OriginalUrl;
        }
    }
}
