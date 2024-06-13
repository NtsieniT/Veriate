using Data.Data;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veriate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {
            var shortUrl = await _urlService.ShortenUrlAsync(request.OriginalUrl, request.ExpirationTime);
            return Ok(new { shortUrl });
        }

        [HttpGet("{alias}")]
        public async Task<IActionResult> RedirectToUrl(string alias)
        {
            var originalUrl = await _urlService.GetOriginalUrlAsync(alias);
            if (originalUrl == null)
            {
                return NotFound();
            }
            return Redirect(originalUrl);
        }
    }

}
