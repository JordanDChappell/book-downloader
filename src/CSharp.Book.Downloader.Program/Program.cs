using System.Text.Json;

using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Core;
using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;

// TODO: command to load a config file once and save
string configJson = File.ReadAllText(args[0]);
JsonSerializerOptions jsonOptions = new() {
    PropertyNameCaseInsensitive = true,
};
DownloadClientConfig? config = JsonSerializer.Deserialize<DownloadClientConfig>(configJson, jsonOptions);

if (config is null)
    return;

IWebDriver driver = WebDriverConfig.GetDriver(config.Browser, false);
BookFinder finder = new(config, driver);

driver.Quit();
