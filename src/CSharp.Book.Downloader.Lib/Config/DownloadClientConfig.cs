using CSharp.Book.Downloader.Lib.Models;

namespace CSharp.Book.Downloader.Lib.Config;

/// <summary>
/// Configuration for the client / site that books will be used to locate books.
/// </summary>
public class DownloadClientConfig {
    /// <summary>
    /// The browser that should be used to access the client / site.
    /// </summary>
    public required BrowserType Browser { get; set; }
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
    public required DownloadClientSelectors Selectors { get; set; }
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
    public required DownloadClientSelector Book { get; set; }
    public required DownloadClientSelector Title { get; set; }
    public required DownloadClientSelector Url { get; set; }
    public DownloadClientSelector? Author { get; set; }
    public DownloadClientSelector? Format { get; set; }
    public DownloadClientSelector? Language { get; set; }
    public DownloadClientSelector? Size { get; set; }
    public DownloadClientSelector? Year { get; set; }
}

public class DownloadClientSelector {
    public required string Value { get; set; }
    public string? Type { get; set; }
    public string? Attribute { get; set; }
    public string? Regex { get; set; }
    public string? Template { get; set; }
}
