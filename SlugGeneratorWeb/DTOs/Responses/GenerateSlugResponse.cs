namespace SlugGeneratorWeb.DTOs.Responses
{
    public class GenerateSlugResponse
    {
        public string OriginalText { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTimeOffset GeneratedAt { get; } = DateTimeOffset.UtcNow;
    }
}
