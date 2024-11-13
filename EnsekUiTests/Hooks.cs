using EnsekUiTests.Pages;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EnsekUiTests
{
    public class Hooks
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void close_Browser()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}