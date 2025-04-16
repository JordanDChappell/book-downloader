namespace CSharp.Book.Downloader.Lib.Models;

public class BookResponse {
    public required string Author { get; set; }
    public required BookFormat Format { get; set; }
    public required string Language { get; set; }
    public required string Publisher { get; set; }
    public required string Size { get; set; }
    public required string Title { get; set; }
    public required string Url { get; set; }
    public required string Year { get; set; }
}
