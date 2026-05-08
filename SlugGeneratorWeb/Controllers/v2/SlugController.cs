using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SlugGeneratorLibrary;
using SlugGeneratorWeb.DTOs.Requests;
using SlugGeneratorWeb.DTOs.Responses;

namespace SlugGeneratorWeb.Controllers.v2
{
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SlugController : ControllerBase
    {
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
