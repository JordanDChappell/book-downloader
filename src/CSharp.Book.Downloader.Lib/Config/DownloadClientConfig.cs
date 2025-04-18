namespace CSharp.Book.Downloader.Lib.Config;

/// <summary>
/// Configuration for the client / site that books will be used to locate books.
/// </summary>
public class DownloadClientConfig {
    /// <summary>
    /// The base URL of the client / site, before any parameters are applied.
    /// </summary>
    public required string BaseUrl { get; set; }
    /// <summary>
    /// Parameter names used to search for books using the client.
    /// </summary>
    public required DownloadClientSearchParameters SearchParameters { get; set; }
    /// <summary>
    /// Selectors used to locate specific fields for books.
    /// </summary>
    public required DownloadClientSearchParameters Selectors { get; set; }
    /// <summary>
    /// A flag that determines if the downloader will attempt to solve cloudflare verification processes.
    /// </summary>
    public bool HasCloudflareVerification { get; set; }
}

/// <summary>
/// Provides parameter names for common search options.
/// </summary>
public class DownloadClientSearchParameters {
    /// <summary>
    /// The base query parameter name to search for a book.
    /// </summary>
    public required string Search { get; set; }
    /// <summary>
    /// Optional book format (filetype) parameter name.
    /// </summary>
    public string? Format { get; set; }
    /// <summary>
    /// Optional language parameter name.
    /// </summary>
    public string? Language { get; set; }
    /// <summary>
    /// Optional book type parameter name.
    /// </summary>
    public string? Type { get; set; }
}

public class DownloadClientSelectors {
    /// <summary>
    /// A selector used to locate individual books in a collection / list.
    /// </summary>
    public required string Book { get; set; }
    public required string Url { get; set; }
    public required string Title { get; set; }
    public string? Author { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }
    public string? Size { get; set; }
    public string? Year { get; set; }
    public string? TitleRegexPattern { get; set; }
    public string? UrlRegexPattern { get; set; }
}
