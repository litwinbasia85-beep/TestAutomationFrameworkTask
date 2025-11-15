using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace CoreLayer.WebDriver
{
    public class Factory
    {
        private const double WaitTimeInSeconds = 30;
        private static string? Browser = Configuration.BrowserType;

        public static IWebDriver CreateWebDriver()
        {
            switch (Browser)
            {
                case "Chrome":
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("disable-infobars");
                        options.AddArgument("--incognito");

                        return new ChromeDriver(service, options, TimeSpan.FromSeconds(WaitTimeInSeconds));
                    }
                case "Edge":
                    var service1 = EdgeDriverService.CreateDefaultService();
                    EdgeOptions options1 = new();
                    options1.AddArgument("inprivate");
                    return new EdgeDriver(service1, options1, TimeSpan.FromSeconds(WaitTimeInSeconds));
                default:
                    throw new ArgumentOutOfRangeException(nameof(Browser), Browser, null);
            }
        }
    }
}
