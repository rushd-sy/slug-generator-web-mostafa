using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SlugGeneratorLibrary;
using SlugGeneratorWeb.Models;

namespace SlugGeneratorWeb.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SlugController : ControllerBase
    {
        [ApiVersion(1, Deprecated = true)]
        [HttpPost]
        public ActionResult<GenerateSlugResponse> SlugifyV1([FromBody] GenerateSlugRequest slugRequest)
        {
            if (String.IsNullOrWhiteSpace(slugRequest.Text))
                return BadRequest("Text should not be empty.");
            char seperator;
            if (slugRequest.Separator is null || slugRequest.Separator == ' ')
                seperator = '-';
            else
                seperator = (char)slugRequest.Separator;
            GenerateSlugResponse slugResponse = new GenerateSlugResponse { 
                OriginalText  = slugRequest.Text,
                Slug = SlugGenerator.CustomGenerate(slugRequest.Text, seperator)
            };
            return Ok(slugResponse);
        }

        [ApiVersion(2)]
        [HttpPost]
        public ActionResult<GenerateSlugResponse> SlugifyV2([FromBody] GenerateSlugRequest slugRequest)
        {
            if (String.IsNullOrWhiteSpace(slugRequest.Text))
                throw new ArgumentException("Text should not be empty.");
            char seperator;
            if (slugRequest.Separator is null || slugRequest.Separator == ' ')
                seperator = '-';
            else
                seperator = (char)slugRequest.Separator;
            GenerateSlugResponse slugResponse = new GenerateSlugResponse
            {
                OriginalText = slugRequest.Text,
                Slug = SlugGenerator.CustomGenerate(slugRequest.Text, seperator)
            };
            return Ok(slugResponse);
        }
    }
}
