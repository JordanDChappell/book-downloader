using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;
using CSharp.Book.Downloader.Lib.Utils;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CSharp.Book.Downloader.Lib.Core;

public class BookDownloader : IBookDownloader {
    private readonly DownloadClientConfig _config;
    private readonly IWebDriver _driver;
    private readonly HttpClient _client;

    public BookDownloader(IConfigLoader configLoader, IWebDriver driver) {
        if (configLoader.Config is null)
            throw new NullReferenceException("A valid configuration has not been loaded, unable to download books");

        _config = configLoader.Config;
        _driver = driver;
        _client = new HttpClient();
    }

    public async Task<FileInfo> DownloadBookAsync(BookResponse book, string? directory = null) {
        string downloadDirectory = (directory ?? _config.DownloadDirectory)
            ?? throw new ArgumentException(
                "Unable to download book as global directory is not set and no override has been provided"
            );

        Directory.CreateDirectory(downloadDirectory);

        if (_config.CanDownloadDirectly)
            return await DownloadFileAsync(book.Url, downloadDirectory);

        _driver.SwitchTo().NewWindow(WindowType.Tab);
        await _driver.Navigate().GoToUrlAsync(book.Url);

        if (_config.HasCloudflareVerification)
            HandleCloudflareVerification();

        string downloadUrl = _driver.FindElementString(_config.Selectors.Download);
        return await DownloadFileAsync(downloadUrl, downloadDirectory);
    }

    private void HandleCloudflareVerification() {
        if (!_driver.FindElements(By.CssSelector(".zone-name-title"), 20).Any())
            return; // can't locate first marker element for cloudflare verification

        IWebElement titleElement = _driver.FindElement(By.CssSelector(".zone-name-title"));

        _driver.PageContains("Verify you are human");

        Actions action = new(_driver);
        action
            .MoveToElement(titleElement)
            .Pause(TimeSpan.FromSeconds(1))
            .Click()
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(Keys.Tab)
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(Keys.Tab)
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(Keys.Space)
            .Perform();

        _driver.SwitchTo().DefaultContent();
    }

    private async Task<FileInfo> DownloadFileAsync(string url, string directory) {
        string fileName = Path.GetFileName(url);
        string downloadPath = Path.Combine(directory, fileName);
        using HttpResponseMessage response = await _client.GetAsync(url);
        using Stream contentStream = await response.Content.ReadAsStreamAsync();
        using (FileStream fileStream = File.OpenWrite(downloadPath)) {
            await contentStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
        return new FileInfo(downloadPath);
    }
}
