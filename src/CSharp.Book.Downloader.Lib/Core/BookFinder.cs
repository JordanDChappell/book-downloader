using System.Collections.Specialized;
using System.Text.RegularExpressions;
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

    public async Task<IEnumerable<BookResponse>> GetBooksAsync(BookRequest request) {
        await Task.CompletedTask;
        List<BookResponse> books = [];

        string searchUrl = BuildSearchUrl(request);
        _driver.Navigate().GoToUrl(searchUrl);

        // Scroll the page to the bottom to materialise all results
        var executor = (IJavaScriptExecutor)_driver;
        executor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

        // Query for book elements and then extract the required information
        IEnumerable<IWebElement> bookElements = _driver.FindElements(By.CssSelector(_config.Selectors.Book.Value));

        foreach (IWebElement bookElement in bookElements) {
            string title = FindElementExtractInformation(bookElement, _config.Selectors.Title);
            string url = FindElementExtractInformation(bookElement, _config.Selectors.Url);

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(url))
                continue;

            bool formatFound = Enum.TryParse(
                FindElementExtractInformation(bookElement, _config.Selectors.Format),
                out BookFormat format
            );

            books.Add(new() {
                Title = title,
                Url = url,
                Author = FindElementExtractInformation(bookElement, _config.Selectors.Author),
                Format = formatFound ? format : null,
                Language = FindElementExtractInformation(bookElement, _config.Selectors.Language),
                Size = FindElementExtractInformation(bookElement, _config.Selectors.Size),
                Year = FindElementExtractInformation(bookElement, _config.Selectors.Year),
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

    private string FindElementExtractInformation(IWebElement book, DownloadClientSelector? selector) {
        if (selector is null)
            return "";

        IWebElement element = book.FindElement(By.CssSelector(selector.Value));

        if (element is null)
            return "";

        return ReplaceIfTemplate(
            FirstRegexMatchIfPattern(
                selector,
                element
            ),
            selector.Template
        );
    }

    private static string FirstRegexMatchIfPattern(DownloadClientSelector selector, IWebElement element) {
        string value = element.GetAttribute(selector.Attribute ?? "") ?? element.Text;
        return string.IsNullOrWhiteSpace(selector.Regex) ? value : Regex.Match(value, selector.Regex).Value;
    }

    private static string ReplaceIfTemplate(string value, string? template) =>
        string.IsNullOrWhiteSpace(template) ? value : template.Replace("*", value);
}
