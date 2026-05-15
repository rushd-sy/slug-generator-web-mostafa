using System.ComponentModel;

namespace SlugGeneratorWeb.DTOs.Requests
{
    public class GenerateSlugRequest
    {
        public string? Text { get; set; } = string.Empty;
        [DefaultValue('-')]
        public char? Separator { get; set; } = '-';
    }
}
