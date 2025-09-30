using System;
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

    public Book? GetBookByISBN(long ISBN) => _repo.GetBookByISBN(ISBN);

    public BookDto AddBook(BookDto book)
    {
        var newBook = MapToBook(book);
        return MapToBookDto(_repo.AddBook(newBook));
    }

    public void DeleteBook(long ISBN) => _repo.DeleteBook(ISBN);

    public Book? UpdateBook(long ISBN, BookDto book)
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
        return _repo.UpdateBook(ISBN, updatedBook);
    }

    public List<Book> GetBooksByAuthor(string author) => _repo.GetBooksByAuthor(author);

    public List<Book> GetBooksByGenre(int genre) => _repo.GetBooksByGenre(genre);

    public List<Book> GetBooksByLanguage(int language) => _repo.GetBooksByLanguage(language);

    public List<Book> GetBooksByStatus(int status) => _repo.GetBooksByStatus(status);

    public List<Book> GetBooksByFormat(int format) => _repo.GetBooksByFormat(format);

    public List<Book> GetBooksByTitle(string title) => _repo.GetBooksByTitle(title);

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
