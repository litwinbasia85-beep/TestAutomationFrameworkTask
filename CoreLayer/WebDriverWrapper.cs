using CoreLayer.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
namespace CoreLayer
{
    public class WebDriverWrapper
    {
        private readonly TimeSpan _timeout;
        private readonly IWebDriver _driver;
        private const double WaitTimeInSeconds = 50;
        private const double IntervalInSeconds = 0.25;
        public WebDriverWrapper(BrowserType browserType)
        {
            _driver = Factory.CreateWebDriver(browserType);
            _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }
        public IWebElement WaitForElementToBePresent(By by)
        {
            var searchPanelWait = new WebDriverWait(this._driver, _timeout);
            searchPanelWait.PollingInterval = TimeSpan.FromSeconds(IntervalInSeconds);
            searchPanelWait.Message = "Element has not been found.";
            IWebElement element = searchPanelWait.Until(_driver => _driver.FindElement(by));
            if (element != null && element.Displayed)
            {
                return element;
            }
            else
            {
                throw new NoSuchElementException("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
            }
        }
        public void StartBrowser()
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }
        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
        public void EnterText(By by, string text)
        {
            var element = WaitForElementToBePresent(by);
            ClearInput(by);
            SendText(element, text);
        }
        public string GetElementText(By by)
        {
            var element = WaitForElementToBePresent(by);
            return element.Text;
        }
        public void Click(By by)
        {
            WaitForElementToBePresent(by)?.Click();
        }

        public string? PlatformName()
        {
            IHasCapabilities? hcDriver = this._driver as IHasCapabilities;
            string? platformName = "";
            object? o = new object();
            if (hcDriver != null)
            {
                o = hcDriver.Capabilities.GetCapability("platformName");
            }
            if (o != null && platformName != null && hcDriver != null)
            {
                platformName = o.ToString();
            }
            else
            {
                throw new Exception("Platform name is null.");
            }
            return platformName;
        }
        public void ClearInput(By by)
        {
            string? cmd = this.PlatformName();
            var element = WaitForElementToBePresent(by);
            if (cmd != null)
            {
                string cmdCtrl = cmd.Contains("mac") ? Keys.Command : Keys.Control;
                var clickAndSendKeyActions = new Actions(_driver);
                clickAndSendKeyActions.Click(element)
                .KeyDown(cmdCtrl)
                .SendKeys("a")
                .KeyUp(cmdCtrl)
                .KeyDown(Keys.Delete)
                .KeyUp(Keys.Delete)
                .Perform();
            }
        }
        public void SendText(IWebElement element, string text)
        {
            string? cmd = this.PlatformName();
            if (cmd != null)
            {
                var clickAndSendKeyActions = new Actions(_driver);
                clickAndSendKeyActions.Click(element)
                            .Pause(TimeSpan.FromSeconds(1))
                            .SendKeys(text)
                            .Perform();
            }
        }
        public void EnterInput(By by)
        {
            var element = WaitForElementToBePresent(by);
            element.SendKeys(Keys.Enter);
        }
        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
