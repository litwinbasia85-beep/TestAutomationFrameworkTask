using OpenQA.Selenium;
using CoreLayer;

namespace BusinessLayer.PageObjects
{
    public class MainPage
    {
        WebDriverWrapper driver;
        private readonly By LogoLocator = By.ClassName("app_logo");
        public MainPage(WebDriverWrapper driver)
        {
            this.driver = driver;
        }
        public string getLogoText()
        {
            return driver.GetElementText(LogoLocator);
        }
    }
}

