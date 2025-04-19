using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Core;
using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;

// TODO: command to load a config file once and save
ConfigLoader loader = new();
loader.LoadConfig(args[0]);

IWebDriver driver = WebDriverConfig.GetDriver(loader.Config.Browser, false);
BookFinder finder = new(loader, driver);

IEnumerable<BookResponse> books = await finder.GetBooksAsync(new() {
    Search = "abc",
});

driver.Quit();
