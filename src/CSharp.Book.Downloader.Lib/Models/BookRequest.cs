namespace CSharp.Book.Downloader.Lib.Models;

public class BookRequest {
    public required string Search { get; set; }
    public IEnumerable<BookFormat> Formats { get; set; } = [];
    public IEnumerable<BookType> Types { get; set; } = [];
    public IEnumerable<string> Languages { get; set; } = [];
}
