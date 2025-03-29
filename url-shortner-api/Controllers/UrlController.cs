using Microsoft.AspNetCore.Mvc;
using url_shortener.Helpers;
using UrlShortner.Helpers;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("/")]
    public class UrlController : ControllerBase
    {
        private readonly DapperHelper dapperHelper;
        private readonly IConfiguration configuration;
        public UrlController(DapperHelper dapperHelper, IConfiguration configuration)
        {
            this.dapperHelper = dapperHelper;
            this.configuration = configuration;
        }

        [HttpGet("shorten")]
        public async Task<IActionResult> ShortenAsync([FromQuery] string url)
        {
            if (url is null) return BadRequest();
            
            UrlShortenerHelper helper = new UrlShortenerHelper(dapperHelper, configuration);
            string shortenUrl = await helper.GenerateShortenUrl(url);
            return Ok(shortenUrl);
        }

        [HttpGet("{code}")]
        public async Task<IResult> RedirectToOriginalUrl(string code)
        {
            UrlShortenerHelper helper = new UrlShortenerHelper(dapperHelper, configuration);
            string url = await helper.GetFullUrlAsync(code);
            if (string.IsNullOrWhiteSpace(url))
            {
                return Results.NotFound();
            }
            return Results.Redirect(url);
        }
    }
}
