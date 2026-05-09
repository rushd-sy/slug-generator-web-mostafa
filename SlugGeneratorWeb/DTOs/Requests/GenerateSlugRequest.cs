using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SlugGeneratorWeb.DTOs.Requests
{
    public class GenerateSlugRequest
    {
        public string Text { get; set; } = string.Empty;
        [DefaultValue('-')]
        public char? Separator { get; set; } = '-';
    }
}
