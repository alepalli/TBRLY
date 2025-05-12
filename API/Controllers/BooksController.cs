using Microsoft.AspNetCore.Mvc;
using TBRly.API.DTOs;
using TBRly.API.Services;

namespace TBRly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_bookService.GetAllBooks());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult Add(BookDto book)
    {
        var newBook = _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _bookService.DeleteBook(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BookDto book)
    {
        var updatedBook = _bookService.UpdateBook(id, book);
        if (updatedBook == null)
        {
            return NotFound();
        }
        return Ok(updatedBook);
    }
}
