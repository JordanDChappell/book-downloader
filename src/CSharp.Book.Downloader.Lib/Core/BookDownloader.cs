using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;
using CSharp.Book.Downloader.Lib.Utils;

using OpenQA.Selenium;

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

    public async Task<FileInfo> DownloadBookAsync(BookResponse book, string? path) {
        if (path is null && _config.DownloadDirectory is null)
            throw new ArgumentException(
                "Unable to download book as global directory is not set and no override has been provided"
            );

        if (!_config.CanDownloadDirectly)
            return await DownloadFileAsync(book.Url, path);

        _driver.SwitchTo().NewWindow(WindowType.Tab);
        await _driver.Navigate().GoToUrlAsync(book.Url);

        if (_config.HasCloudflareVerification) {
            IWebElement iframe = _driver.FindElement(By.CssSelector("iframe[title*='Cloudflare']"), 20);
            _driver.SwitchTo().Frame(iframe);
            _driver.FindElement(By.CssSelector("input"), 20).Click();
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
        }

        string downloadUrl = _driver.FindElementString(_config.Selectors.Download);
        return await DownloadFileAsync(downloadUrl, path);
    }

    private async Task<FileInfo> DownloadFileAsync(string url, string? path) {
        string fileName = Path.GetFileName(url);
        string downloadPath = path ?? Path.Combine(_config.DownloadDirectory ?? "", fileName);
        using HttpResponseMessage response = await _client.GetAsync(url);
        using Stream contentStream = await response.Content.ReadAsStreamAsync();
        using (FileStream fileStream = File.OpenWrite(downloadPath)) {
            await contentStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
        return new FileInfo(downloadPath);
    }
}
