using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using CoreLayer;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;

namespace BusinessLayer.PageObjects
{
    public class LoginPage
    {
        private WebDriverWrapper driver;

        public LoginPage(WebDriverWrapper driver)
        {
            this.driver = driver;
        }

        private readonly By userNameLocator = By.Id("user-name");
        private readonly By passwordLocator = By.Id("password");
        private readonly By LoginButtonLocator = By.Id("login-button");

        private readonly By ErrorMessageLocator = By.ClassName("error-message-container");

        public MainPage Login(string login, string password)
        {
            IWebElement el = driver.WaitForElementToBePresent(userNameLocator);
            driver.EnterText(userNameLocator, login);
            driver.EnterText(passwordLocator, password);
            driver.Click(LoginButtonLocator);

            return new MainPage(driver);
        }
        public LoginPage ErrorLogin(string login, string password, int TestNumber)
        {
            driver.EnterText(userNameLocator, login);
            //  driver.EnterText(passwordLocator, password);
            switch (TestNumber)
            {
                case 1:
                    driver.EnterText(passwordLocator, password);
                    driver.ClearInput(userNameLocator);
                    driver.ClearInput(passwordLocator);
                    break;
                case 2:
                    driver.ClearInput(passwordLocator);
                    break;
                default:
                    break;
            }
            driver.Click(LoginButtonLocator);
            return new LoginPage(driver);
        }
        public string getErrorMessage()
        {
            return driver.GetElementText(ErrorMessageLocator);
        }
    }
}
