using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekUiTests.Pages
{
    public class OrderPage
    {
        private IWebDriver _driver;
        private String orderPageUrl = "https://ensekautomationcandidatetest.azurewebsites.net/Energy/Buy";

        public OrderPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By resetBtn = By.Name("Reset");
        private By elecBuyBtn = By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[3]/td[5]/input");
        private By gasBuyBtn= By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[5]/input");
        private By oilBuyBtn = By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[4]/td[5]/input");
        private By quantityInputs = By.Id("energyType_AmountPurchased");
        private By gasQuantity = By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]");
        private By elecQuantity = By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[3]/td[3]");
        private By oilQuantity = By.XPath("/html/body/div[2]/div[1]/div[1]/table/tbody/tr[4]/td[3]");


        public void Goto()
        {
            _driver.Navigate().GoToUrl(orderPageUrl);
        }

        public void CheckCurrentPage(string expectedPageTitle)
        {
            string currentPageTitle = _driver.Title.ToString();
            currentPageTitle.Should().Be(expectedPageTitle);
        }

        public void ResetData()
        {
            _driver.FindElement(resetBtn).Click();
        }

        public void BuySomeEnergy(string energyType, int amount)
        {
            switch (energyType)
            {
                case "Gas":
                    _driver.FindElements(quantityInputs)[0].SendKeys(amount.ToString());
                    _driver.FindElement(gasBuyBtn).Click();
                    break;
                case "Electricity":
                    _driver.FindElements(quantityInputs)[1].SendKeys(amount.ToString());
                    _driver.FindElement(elecBuyBtn).Click();
                    break;
                case "Oil":
                    _driver.FindElements(quantityInputs)[2].SendKeys(amount.ToString());
                    _driver.FindElement(oilBuyBtn).Click();
                    break;
                default:
                    throw new Exception("You have selected an energy type that does not exist");
            }
        }

        public string GetCurrentQuantity(string energyType)
        {
            string quantity;
            switch (energyType)
            {
                case "Gas":
                    quantity = _driver.FindElement(gasQuantity).Text;
                    break;
                case "Electricity":
                    quantity = _driver.FindElement(elecQuantity).Text;
                    break;
                case "Oil":
                    quantity = _driver.FindElement(oilQuantity).Text;
                    break;
                default:
                    quantity = "0";
                    break;
            }
            return quantity;
        }
    }
}
