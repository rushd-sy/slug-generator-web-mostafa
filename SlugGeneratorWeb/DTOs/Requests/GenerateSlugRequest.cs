using System.ComponentModel;

namespace SlugGeneratorWeb.DTOs.Requests
{
    public class GenerateSlugRequest
    {
<<<<<<< HEAD
        public string? Text { get; set; } = string.Empty;
=======
        public string Text { get; set; } = string.Empty;
>>>>>>> main
        [DefaultValue('-')]
        public char? Separator { get; set; } = '-';
    }
}
