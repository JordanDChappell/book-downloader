using System.Collections.Specialized;
using System.Web;

using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;
using CSharp.Book.Downloader.Lib.Utils;

using OpenQA.Selenium;

namespace CSharp.Book.Downloader.Lib.Core;

public class BookFinder : IBookFinder {
    private readonly DownloadClientConfig _config;
    private readonly IWebDriver _driver;

    public BookFinder(DownloadClientConfig config, IWebDriver driver) {
        _config = config;
        _driver = driver;
    }

    public async Task<IEnumerable<BookResponse>> GetBooksAsync(BookRequest request) {
        List<BookResponse> books = [];

        await _driver.Navigate().GoToUrlAsync(BuildSearchUrl(request));

        // Scroll the page to the bottom to materialise all results
        var executor = (IJavaScriptExecutor)_driver;
        executor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

        // Query for book elements and then extract the required information
        IEnumerable<IWebElement> bookElements = _driver.FindElements(By.CssSelector(_config.Selectors.Book.Value), 20);

        foreach (IWebElement bookElement in bookElements) {
            string title = _driver.FindElementString(bookElement, _config.Selectors.Title);
            string url = _driver.FindElementString(bookElement, _config.Selectors.Url);

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(url))
                continue;

            bool formatFound = Enum.TryParse(
                _driver.FindElementString(bookElement, _config.Selectors.Format),
                out BookFormat format
            );

            books.Add(new() {
                Title = title,
                Url = url,
                Author = _driver.FindElementString(bookElement, _config.Selectors.Author),
                Format = formatFound ? format : null,
                Language = _driver.FindElementString(bookElement, _config.Selectors.Language),
                Size = _driver.FindElementString(bookElement, _config.Selectors.Size),
                Year = _driver.FindElementString(bookElement, _config.Selectors.Year),
            });
        }

        return books;
    }

    private string BuildSearchUrl(BookRequest request) {
        DownloadClientSearchParameters parameters = _config.SearchParameters;
        UriBuilder builder = new(_config.BaseUrl);

        NameValueCollection queryParams = HttpUtility.ParseQueryString(builder.Query);
        queryParams[parameters.Search] = request.Search;
        AppendParams(queryParams, parameters.Format, request.Formats);
        AppendParams(queryParams, parameters.Language, request.Languages);
        AppendParams(queryParams, parameters.Type, request.Types);

        builder.Query = queryParams.ToString();
        return builder.ToString();
    }

    private static void AppendParams<T>(NameValueCollection queryParams, string? paramName, IEnumerable<T> values) {
        if (!string.IsNullOrWhiteSpace(paramName) && values.Any())
            foreach (T format in values)
                queryParams[paramName] = format?.ToString();
    }
}
