using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

