using CoreLayer;
using BusinessLayer.PageObjects;
using NUnit.Framework.Internal;

namespace TestLayer
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Tests
    {
        public WebDriverWrapper Browser { get; private set; }
        BrowserType val;
        public Tests(BrowserType type)
        {
            this.val = type;
        }
        [SetUp]
        public void Setup()
        {
            Browser = new WebDriverWrapper(val);
            Browser.StartBrowser();
            Browser.NavigateTo(Configuration.AppUrl);
        }
        [Test]
        public void Test1()
        {
            LoginPage loginPage = new LoginPage(Browser);
            string admin = "admin";
            string password = "password";
            string error_substring = "Username is required";

            var ErrorLogin = loginPage.ErrorLogin(admin, 1, password).getErrorMessage();

            Assert.That(ErrorLogin.Contains(error_substring));
            Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            LoginPage loginPage = new LoginPage(Browser);
            string admin = "admin";
            string error_substring = "Password is required";

            var ErrorLogin = loginPage.ErrorLogin(admin, 2).getErrorMessage();

            Assert.That(ErrorLogin.Contains(error_substring));
            Assert.Pass();
        }

        [TestCase("standard_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        [TestCase("performance_glitch_user", "secret_sauce")]
        [TestCase("error_user", "secret_sauce")]
        [TestCase("visual_user", "secret_sauce")]
        public void Test3(string admin, string password)
        {
            string logo_substring = "Swag Labs";

            LoginPage loginPage = new LoginPage(Browser);
            var LogoText = loginPage.Login(admin, password).getLogoText();

            Assert.That(LogoText.Contains(logo_substring));
            Assert.Pass();
        }
        [TearDown]
        public void Teardown()
        {
            Browser.Close();
        }
    }

}