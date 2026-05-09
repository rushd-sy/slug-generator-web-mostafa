using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SlugGeneratorLibrary;
using SlugGeneratorWeb.DTOs.Responses;
using SlugGeneratorWeb.DTOs.Requests;

namespace SlugGeneratorWeb.Controllers.v1
{
    [ApiVersion(1, Deprecated = true)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SlugController : ControllerBase
    {
        [ApiVersion(1, Deprecated = true)]
        [HttpPost]
        public ActionResult<GenerateSlugResponse> Slugify([FromBody] GenerateSlugRequest slugRequest)
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
    }
}
