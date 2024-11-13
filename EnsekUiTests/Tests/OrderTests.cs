using EnsekUiTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekUiTests.Tests
{
    internal class OrderTests : Hooks
    {
        [Test]
        [TestCase(1, "Gas")]
        [TestCase(1, "Electricity")]
        [TestCase(1, "Oil")]
        public void Order1Unit(int amountOfUnits, string energyType)
        {
            HomePage homePage = new HomePage(driver);
            OrderPage orderPage = new OrderPage(driver);
            SaleConfirmedPage saleConfirmedPage = new SaleConfirmedPage(driver);
            //Arrange
            // Navigate to the buy page
            homePage.Goto();
            homePage.CheckCurrentPage("ENSEK Energy Corp. - Candidate Test");
            homePage.ClickBuyButton();
            //Check current page title
            orderPage.CheckCurrentPage("Buy - Candidate Test");
            //Resets the data so test data is reliable
            orderPage.ResetData();
            // Store how many of energy type is available
            string currentQuantity = orderPage.GetCurrentQuantity(energyType);

            //Act
            // Enter a value into the relevant field
            // Click the buy button
            orderPage.BuySomeEnergy(energyType, amountOfUnits);

            //Assert
            //Check current page title
            saleConfirmedPage.CheckCurrentPage("Sale Confirmed - Candidate Test");

            // Check amount remaining against original value
            int currentQuantityInt = int.Parse(currentQuantity);
            saleConfirmedPage.CheckUnitsOrdered(amountOfUnits);
            saleConfirmedPage.CheckUnitsRemaining(currentQuantityInt, amountOfUnits);
        }
    }
}
