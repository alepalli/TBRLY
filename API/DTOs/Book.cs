using TBRly.API.Enums;

namespace TBRly.API.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public BookFormat Format { get; set; }
        public BookStatus Status { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public BookLanguage Language { get; set; }
        public BookGenre Genre { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
