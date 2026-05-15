using Asp.Versioning;
using FluentValidation;
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
        private IValidator<GenerateSlugRequest> _validator;

        public SlugController(IValidator<GenerateSlugRequest> validator)
        {
            _validator = validator;
        }

        [ApiVersion(2.1)]
        [HttpPost]
        public ActionResult<GenerateSlugResponse> Slugify([FromBody] GenerateSlugRequest slugRequest)
        {
            var validationResult = _validator.Validate(slugRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            char separator;
            if (slugRequest.Separator is null)
                separator = '-';
            else
                separator = (char)slugRequest.Separator;
            GenerateSlugResponse slugResponse = new GenerateSlugResponse
            {
                OriginalText = slugRequest.Text!,
                Slug = SlugGenerator.CustomGenerate(slugRequest.Text!, separator)
            };
            return Ok(slugResponse);
        }
    }
}
