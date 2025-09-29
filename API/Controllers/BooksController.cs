using Microsoft.AspNetCore.Mvc;
using TBRly.API.DTOs;
using TBRly.API.Services;

namespace TBRly.API.Controllers;

[ApiController]
[Route("api/[controller]")] // significa che il percorso base è api/books
// [controller] prende il nome della classe senza il suffisso Controller.
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAllBooks() => Ok(_bookService.GetAllBooks());

    [HttpGet("{ISBN}")]
    public IActionResult GetBookByISBN(long ISBN)
    {
        var book = _bookService.GetBookByISBN(ISBN);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook(BookDto book)
    {
        var newBook = _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetBookByISBN), new { ISBN = newBook.ISBN }, newBook);
    }

    [HttpDelete("{ISBN}")]
    public IActionResult DeleteBook(long ISBN)
    {
        _bookService.DeleteBook(ISBN);
        return Ok("Libro eliminato con successo");
    }

    [HttpPut("{ISBN}")]
    public IActionResult UpdateBook(long ISBN, BookDto book)
    {
        var updatedBook = _bookService.UpdateBook(ISBN, book);
        if (updatedBook == null)
        {
            return NotFound();
        }
        return Ok(updatedBook);
    }

    [HttpGet("author/{author}")]
    public IActionResult GetBooksByAuthor(string author)
    {
        var books = _bookService.GetBooksByAuthor(author);
        if (books == null || books.Count == 0)
        {
            return NotFound();
        }
        return Ok(books);
    }
}
