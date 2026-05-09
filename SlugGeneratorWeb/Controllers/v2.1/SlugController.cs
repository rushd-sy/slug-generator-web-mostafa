using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SlugGeneratorLibrary;
using SlugGeneratorWeb.DTOs.Requests;
using SlugGeneratorWeb.DTOs.Responses;
using SlugGeneratorWeb.Validation;

namespace SlugGeneratorWeb.Controllers.v2_1
{
    [ApiVersion(2.1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SlugController : ControllerBase
    {
        [ApiVersion(2.1)]
        [HttpPost]
        public ActionResult<GenerateSlugResponse> Slugify([FromBody] GenerateSlugRequest slugRequest)
        {
            char seperator = slugRequest.Separator is null ?
                seperator = '-' : (char)slugRequest.Separator;
            GenerateSlugResponse slugResponse = new GenerateSlugResponse
            {
                OriginalText = slugRequest.Text!,
                Slug = SlugGenerator.CustomGenerate(slugRequest.Text!, seperator)
            };
            return Ok(slugResponse);
        }
    }
}
