using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekUiTests.Pages
{
    public class SaleConfirmedPage
    {
        private IWebDriver _driver;

        public SaleConfirmedPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By unitsOrderedText = By.CssSelector(".well");
        private By unitsRemainingText = By.XPath("/html/body/div[2]/div/div/p");

        public void CheckCurrentPage(string expectedPageTitle)
        {
            string currentPageTitle = _driver.Title.ToString();
            currentPageTitle.Should().Be(expectedPageTitle);
        }

        public void CheckUnitsOrdered(int unitsOrdered)
        {
            string unitsOrderedString = _driver.FindElement(unitsOrderedText).Text;
            unitsOrderedString.Contains(unitsOrdered.ToString());
        }

        public void CheckUnitsRemaining(int currentOrders, int unitsOrdered)
        {
            int expectedRemainingUnits = currentOrders- unitsOrdered;
            string unitsRemainingString = _driver.FindElement(unitsRemainingText).Text;
            unitsRemainingString.Should().Contain(expectedRemainingUnits.ToString());
        }
    }
}
