using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekUiTests.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;
        private String homePageUrl = "https://ensekautomationcandidatetest.azurewebsites.net/";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By buyButton = By.CssSelector("a[href='/Energy/Buy']");

        public void Goto()
        {
            _driver.Navigate().GoToUrl(homePageUrl);
        }

        public void CheckCurrentPage(string expectedPageTitle)
        {
            string currentPageTitle = _driver.Title.ToString();
            currentPageTitle.Should().Be(expectedPageTitle);
        }

        public void ClickBuyButton()
        {
            _driver.FindElement(buyButton).Click();
        }
    }
}
