using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace CoreLayer.WebDriver
{
    public class Factory
    {
        private const double WaitTimeInSeconds = 30;
        public static IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("disable-infobars");
                        options.AddArgument("--incognito");

                        return new ChromeDriver(service, options, TimeSpan.FromSeconds(WaitTimeInSeconds));
                    }
                case BrowserType.Edge:
                    var service1 = EdgeDriverService.CreateDefaultService();
                    EdgeOptions options1 = new();
                    options1.AddArgument("inprivate");
                    return new EdgeDriver(service1, options1, TimeSpan.FromSeconds(WaitTimeInSeconds));
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}
