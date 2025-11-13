using OpenQA.Selenium;
using CoreLayer;

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
            driver.EnterText(userNameLocator, login);
            driver.EnterText(passwordLocator, password);
            driver.Click(LoginButtonLocator);

            return new MainPage(driver);
        }
        public LoginPage ErrorLogin(string login, int TestNumber, string password = "")
        {
            driver.EnterText(userNameLocator, login);
            switch (TestNumber)
            {
                case 1:
                    driver.EnterText(passwordLocator, password);
                    driver.ClearInput(userNameLocator);
                    driver.ClearInput(passwordLocator);
                    break;
                case 2:
                    driver.Equals(passwordLocator);
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
