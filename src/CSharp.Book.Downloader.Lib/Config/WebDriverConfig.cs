using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CSharp.Book.Downloader.Lib.Config;

public static class WebDriverConfig {
    public static IWebDriver GetDriver(BrowserType browser, bool headless = true) {
        DriverManager driverManager = new();

        switch (browser) {
            case BrowserType.Chrome: {
                driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetChromeDriver(headless);
            }
            case BrowserType.Edge: {
                driverManager.SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetEdgeDriver(headless);
            }
            case BrowserType.Firefox: {
                driverManager.SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetFirefoxDriver(headless);
            }
            default: {
                throw new NotSupportedException("The provided browser type is not supported");
            }
        }
    }

    private static ChromeDriver GetChromeDriver(bool headless) {
        ChromeOptions options = new();
        if (headless)
            options.AddArguments("headless", "disable-gpu");
        return new ChromeDriver(options);
    }

    private static EdgeDriver GetEdgeDriver(bool headless) {
        EdgeOptions options = new();
        if (headless)
            options.AddArguments("headless", "disable-gpu");
        return new EdgeDriver(options);
    }

    private static FirefoxDriver GetFirefoxDriver(bool headless) {
        FirefoxOptions options = new();
        if (headless)
            options.AddArguments("headless");
        return new FirefoxDriver(options);
    }
}
