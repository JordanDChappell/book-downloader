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

/// <summary>
/// Selectors for the chosen download client / site.
/// </summary>
public class DownloadClientSelectors {
    /// <summary>
    /// A selector used to locate individual books in a collection / list.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public required StringSelector Book { get; set; }
    /// <summary>
    /// A selector used to find the book's title, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public required StringSelector Title { get; set; }
    /// <summary>
    /// A selector used to find the book's url, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public required StringSelector Url { get; set; }
    /// <summary>
    /// A selector used to find the book's author, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public StringSelector? Author { get; set; }
    /// <summary>
    /// A selector used to find the book's format / file type, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public StringSelector? Format { get; set; }
    /// <summary>
    /// A selector used to find the book's lamguage, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public StringSelector? Language { get; set; }
    /// <summary>
    /// A selector used to find the book's size, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public StringSelector? Size { get; set; }
    /// <summary>
    /// A selector used to find the book's year, called on the element found by the `Book` selector.
    /// <para>Note: this selector is required.</para>
    /// </summary>
    public StringSelector? Year { get; set; }
}
