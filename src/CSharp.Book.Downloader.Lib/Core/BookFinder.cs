using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;

namespace CSharp.Book.Downloader.Lib.Core;

public class BookFinder : IBookFinder {
    public Task<BookResponse> GetBooksAsync(BookRequest request) {
        throw new NotImplementedException();
    }
}
