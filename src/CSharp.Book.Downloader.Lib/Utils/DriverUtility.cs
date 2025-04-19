using System.Text.RegularExpressions;

using CSharp.Book.Downloader.Lib.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSharp.Book.Downloader.Lib.Utils;

/// <summary>
/// Provides extension methods / utility methods for an `IWebDriver` instance.
/// </summary>
public static class DriverUtility {
    public static IWebElement FindElement(this IWebDriver driver, By by, int waitTimeSeconds = 20) =>
        driver.WaitUntil(driver => driver.FindElement(by), waitTimeSeconds);

    public static IWebElement FindElement(this IWebDriver driver, IWebElement element, By by, int waitTimeSeconds = 20) =>
        driver.WaitUntil(_ => element.FindElement(by), waitTimeSeconds);

    public static IEnumerable<IWebElement> FindElements(this IWebDriver driver, By by, int waitTimeSeconds = 20) =>
        driver.WaitUntil(driver => driver.FindElements(by), waitTimeSeconds);

    public static IEnumerable<IWebElement> FindElements(this IWebDriver driver, IWebElement element, By by, int waitTimeSeconds = 20) =>
        driver.WaitUntil(_ => element.FindElements(by), waitTimeSeconds);

    public static string FindElementString(this IWebDriver driver, StringSelector? selector, int waitTimeSeconds = 20) {
        if (selector is null)
            return "";

        IWebElement element = driver.FindElement(GetSelector(selector), waitTimeSeconds);
        return ExtractElementString(element, selector);
    }

    public static string FindElementString(this IWebDriver driver, IWebElement parent, StringSelector? selector, int waitTimeSeconds = 20) {
        if (selector is null)
            return "";

        IWebElement element = driver.FindElement(parent, GetSelector(selector), waitTimeSeconds);
        return ExtractElementString(element, selector);
    }

    public static By GetSelector(StringSelector selector) => selector.Type switch {
        SelectorType.Css => By.CssSelector(selector.Value),
        SelectorType.Id => By.Id(selector.Value),
        SelectorType.XPath => By.XPath(selector.Value),
        _ => throw new NotSupportedException("The provided selector type is not supported"),
    };

    public static string ExtractElementString(IWebElement element, StringSelector selector) {
        string match = RegexMatchIfPattern(selector, element);
        return ReplaceIfTemplate(match, selector.Template);
    }

    public static string RegexMatchIfPattern(StringSelector selector, IWebElement element) {
        string value = element.GetAttribute(selector.Attribute ?? "") ?? element.Text;
        return string.IsNullOrWhiteSpace(selector.Regex) ? value : Regex.Match(value, selector.Regex).Value;
    }

    public static string ReplaceIfTemplate(string value, string? template) =>
        string.IsNullOrWhiteSpace(template) ? value : template.Replace("*", value);

    private static TReturn WaitUntil<TReturn>(
        this IWebDriver driver,
        Func<IWebDriver, TReturn> action,
        int waitTimeSeconds
    ) {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(waitTimeSeconds));
        return wait.Until(action);
    }
}
