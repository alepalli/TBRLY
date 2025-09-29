using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBRly.API.Enums;

namespace TBRly.API.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // disabilita AUTO_INCREMENT
    public long ISBN { get; set; } // chiave primaria
    public string Title { get; set; } = string.Empty; // valorizzare una stringa vuota
    public string Author { get; set; } = string.Empty;
    public BookFormat Format { get; set; }
    public BookStatus Status { get; set; }
    public BookLanguage Language { get; set; }
    public BookGenre Genre { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string Review { get; set; } = string.Empty;
}
