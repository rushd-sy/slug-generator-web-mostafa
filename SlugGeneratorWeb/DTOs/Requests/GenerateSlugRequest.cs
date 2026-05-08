using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SlugGeneratorWeb.DTOs.Requests
{
    public class GenerateSlugRequest
    {
        [MinLength(1)]
        public string Text { get; set; } = string.Empty;
        [StringLength(1)]
        [DefaultValue('-')]
        public char? Separator { get; set; } = '-';
    }
}
