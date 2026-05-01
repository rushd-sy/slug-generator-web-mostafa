using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SlugGeneratorLibrary;
using SlugGeneratorWeb.Models;

namespace SlugGeneratorWeb.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SlugController : ControllerBase
    {
        [HttpPost]
        public ActionResult<GenerateSlugResponse> Slugify([FromBody] GenerateSlugRequest slugRequest)
        {
            if (String.IsNullOrWhiteSpace(slugRequest.Text))
                return BadRequest("Text should not be empty.");
            if (slugRequest.Separator is null || slugRequest.Separator == ' ')
                slugRequest.Separator = '-';
            GenerateSlugResponse slugResponse = new GenerateSlugResponse();
            slugResponse.OriginalText = slugRequest.Text;
            slugResponse.Slug = SlugGenerator.CustomGenerate(slugRequest.Text, 
                (char)slugRequest.Separator);
            return Ok(slugResponse);
        }
    }
}
