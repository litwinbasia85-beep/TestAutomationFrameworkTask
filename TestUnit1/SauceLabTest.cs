using BusinessLayer.PageObjects;
using CoreLayer;

namespace TestProject1
{
    public class Tests
    {
        public WebDriverWrapper Browser;
        public string error_login = "";
        public string error_password = "";
        public static object[] objects = [];
        [SetUp]
        public void Setup()
        {
            Browser = new WebDriverWrapper();
            Browser.StartBrowser();
            if (Configuration.AppUrl != null)
            {
                Browser.NavigateTo(Configuration.AppUrl);
            }
            if (Configuration.ErrorLogin != null)
            {
                error_login = Configuration.ErrorLogin;
            }
            if (Configuration.ErrorPassword != null)
            {
                error_password = Configuration.ErrorPassword;
            }
        }
        public static IEnumerable<string[]> StringsToTest()
        {
            if (Configuration.AllUsersJson != null)
            {
                for (int i = 0; i < Configuration.AllUsersJson.Length; i++)
                {
                    {
                        yield return Configuration.AllUsersJson[i];
                    }
                }
            }
        }
        [Test]
        public void ErrorLoginAndPassword()
        {
            LoginPage loginPage = new LoginPage(Browser);
            string error_substring = "Username is required";

            var ErrorLogin = loginPage.ErrorLogin(error_login, 1, error_password).getErrorMessage();

            Assert.That(ErrorLogin.Contains(error_substring));
        }
        [Test]
        public void ErrorLoginNoPassword()
        {
            LoginPage loginPage = new LoginPage(Browser);
            string error_substring = "Password is required";

            var ErrorLogin = loginPage.ErrorLogin(error_login, 2).getErrorMessage();

            Assert.That(ErrorLogin.Contains(error_substring));
        }
        [TestCaseSource(nameof(StringsToTest))]
        public void Login_LogoContainString(string admin, string password)
        {
            string logo_substring = "Swag Labs";

            LoginPage loginPage = new LoginPage(Browser);
            var LogoText = loginPage.Login(admin, password).getLogoText();

            Assert.That(LogoText.Contains(logo_substring));
        }
        [TearDown]
        public void Teardown()
        {
            Browser.Close();
        }
    }
}