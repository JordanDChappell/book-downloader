namespace CSharp.Book.Downloader.Lib.Config;

/// <summary>
/// Configuration for the client / site that books will be located within.
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
    public required string SearchParameterName { get; set; }
    /// <summary>
    /// Optional book format (filetype) parameter name.
    /// </summary>
    public string? FormatParameterName { get; set; }
    /// <summary>
    /// Optional book type parameter name.
    /// </summary>
    public string? TypeParameterName { get; set; }
    /// <summary>
    /// Optional language parameter name.
    /// </summary>
    public string? LanguageParameterName { get; set; }
}

public class DownloadClientSelectors {
    /// <summary>
    /// A selector used to locate individual books in a collection / list.
    /// </summary>
    public required string BookSelector { get; set; }
    public required string DownloadLinkSelector { get; set; }
    public required string TitleSelector { get; set; }
    public string? AuthorSelector { get; set; }
    public string? LanguageSelector { get; set; }
    public string? PublisherSelector { get; set; }
    public string? SizeSelector { get; set; }
    public string? YearSelector { get; set; }
    public string? BookTitleRegexPattern { get; set; }
    public string? DownloadLinkRegexPattern { get; set; }
}
