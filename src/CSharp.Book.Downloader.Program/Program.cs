using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Core;
using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;

using Selenium.WebDriver.UndetectedChromeDriver;

// TODO: command to load a config file once and save

// TODO: dependency injection?

// TODO: System.CommandLine commands


ConfigLoader loader = new();
DownloadClientConfig config = loader.LoadConfig(args[0]);

IWebDriver driver = UndetectedChromeDriver.Instance();
BookFinder finder = new(loader, driver);
BookDownloader downloader = new(loader, driver);

try {
    IEnumerable<BookResponse> books = await finder.GetBooksAsync(new() {
        Search = args[1],
    });

    if (!books.Any())
        throw new NotFoundException("Unable to locate any books to download for the provided query");

    await downloader.DownloadBookAsync(books.First(), args[2]);
} finally {
    driver.Quit();
}
