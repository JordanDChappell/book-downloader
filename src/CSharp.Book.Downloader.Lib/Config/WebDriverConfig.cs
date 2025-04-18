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
    public static IWebDriver GetDriver(BrowserType browser) {
        DriverManager driverManager = new();

        switch (browser) {
            case BrowserType.Chrome: {
                driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetChromeDriver();
            }
            case BrowserType.Edge: {
                driverManager.SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetEdgeDriver();
            }
            case BrowserType.Firefox: {
                driverManager.SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                return GetFirefoxDriver();
            }
            default: {
                throw new NotSupportedException("The provided browser type is not supported");
            }
        }
    }

    private static ChromeDriver GetChromeDriver() {
        ChromeOptions options = new();
#if DEBUG
        options.AddArguments("headless", "disable-gpu");
#endif
        return new ChromeDriver(options);
    }

    private static EdgeDriver GetEdgeDriver() {
        EdgeOptions options = new();
#if DEBUG
        options.AddArguments("headless", "disable-gpu");
#endif
        return new EdgeDriver(options);
    }

    private static FirefoxDriver GetFirefoxDriver() {
        FirefoxOptions options = new();
#if DEBUG
        options.AddArguments("headless");
#endif
        return new FirefoxDriver(options);
    }
}
