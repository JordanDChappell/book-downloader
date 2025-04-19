using System.Text.Json;

using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Core;
using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;

string configJson = File.ReadAllText(args[0]);
JsonSerializerOptions jsonOptions = new() {
    PropertyNameCaseInsensitive = true,
};
DownloadClientConfig? config = JsonSerializer.Deserialize<DownloadClientConfig>(configJson, jsonOptions);

if (config is null)
    return;

IWebDriver driver = WebDriverConfig.GetDriver(config.Browser, false);
BookFinder finder = new(config, driver);
IEnumerable<BookResponse> books = await finder.GetBooksAsync(new BookRequest() {
    Search = "Locke Lamora",
});

foreach (BookResponse book in books)
    Console.WriteLine(book);

driver.Quit();
