namespace SlugGeneratorWeb.Models
{
    public class GenerateSlugRequest
    {
        public string Text { get; set; } = string.Empty;
        public char? Separator { get; set; }
    }

    public class GenerateSlugResponse
    {
        public string OriginalText { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; } = DateTime.Now;
    }
}
