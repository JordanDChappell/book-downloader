namespace CSharp.Book.Downloader.Lib.Models;

public class BookResponse {
    public required string Title { get; set; }
    public required string Url { get; set; }
    public string? Author { get; set; }
    public BookFormat? Format { get; set; }
    public string? Language { get; set; }
    public string? Size { get; set; }
    public string? Year { get; set; }

    public override string ToString() => $"{Title} - {Author} - {Url}";
}
