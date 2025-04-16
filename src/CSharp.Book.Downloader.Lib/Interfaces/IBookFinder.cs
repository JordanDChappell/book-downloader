using CSharp.Book.Downloader.Lib.Models;

namespace CSharp.Book.Downloader.Lib.Interfaces;

public interface IBookFinder {
    Task<BookResponse> GetBooksAsync(BookRequest request);
}
