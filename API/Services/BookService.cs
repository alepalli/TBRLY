using System.Collections.Generic;
using TBRly.API.DTOs;
using TBRly.API.Models;
using TBRly.API.Repositories;

namespace TBRly.API.Services;

public class BookService
{
    private readonly IBookRepository _repo;

    public BookService(IBookRepository repo)
    {
        _repo = repo;
    }

    public List<BookDto> GetAllBooks() =>
        _repo
            .GetAllBooks()
            .ConvertAll(book => new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                Format = book.Format,
                Status = book.Status,
                ISBN = book.ISBN,
                Language = book.Language,
                Genre = book.Genre,
                Description = book.Description,
                CoverImageUrl = book.CoverImageUrl,
                PageCount = book.PageCount,
                Review = book.Review,
            });

    public Book? GetBookById(int id) => _repo.GetBookById(id);

    public BookDto AddBook(BookDto book)
    {
        var newBook = MapToBook(book);
        return MapToBookDto(_repo.AddBook(newBook));
    }

    public void DeleteBook(int id) => _repo.DeleteBook(id);

    public Book? UpdateBook(int id, BookDto book)
    {
        var updatedBook = new Book
        {
            Title = book.Title,
            Author = book.Author,
            Format = book.Format,
            Status = book.Status,
            ISBN = book.ISBN,
            Language = book.Language,
            Genre = book.Genre,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            PageCount = book.PageCount,
            Review = book.Review,
        };
        return _repo.UpdateBook(id, updatedBook);
    }

    public Book MapToBook(BookDto bookDto)
    {
        return new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author,
            Format = bookDto.Format,
            Status = bookDto.Status,
            ISBN = bookDto.ISBN,
            Language = bookDto.Language,
            Genre = bookDto.Genre,
            Description = bookDto.Description,
            CoverImageUrl = bookDto.CoverImageUrl,
            PageCount = bookDto.PageCount,
            Review = bookDto.Review,
        };
    }

    public BookDto MapToBookDto(Book book)
    {
        return new BookDto
        {
            Title = book.Title,
            Author = book.Author,
            Format = book.Format,
            Status = book.Status,
            ISBN = book.ISBN,
            Language = book.Language,
            Genre = book.Genre,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            PageCount = book.PageCount,
            Review = book.Review,
        };
    }
}
