using System.Collections.Specialized;
using System.Web;

using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;

namespace CSharp.Book.Downloader.Lib.Core;

public class BookFinder : IBookFinder {
    private readonly DownloadClientConfig _config;
    private readonly IWebDriver _driver;

    public BookFinder(DownloadClientConfig config, IWebDriver driver) {
        _config = config;
        _driver = driver;
    }

    public async Task<BookResponse> GetBooksAsync(BookRequest request) {
        await Task.CompletedTask;

        string searchUrl = BuildSearchUrl(request);
        _driver.Navigate().GoToUrl(searchUrl);

        return new BookResponse() {
            Title = "Test",
            Url = "https://path/to/test",
        };
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
