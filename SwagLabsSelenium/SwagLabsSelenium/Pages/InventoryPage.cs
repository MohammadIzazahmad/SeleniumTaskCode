using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsSelenium.Pages
{
    public class InventoryPage
    {
        private IWebDriver driver;

        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[@id='add-to-cart-sauce-labs-backpack']")]
        private IWebElement btnAddToCart_Item { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='shopping_cart_link']/span[text()='1']")]
        private IWebElement lnkCartWith_OneItem { get; set; }

        [FindsBy(How = How.Id, Using = "react-burger-menu-btn")]
        private IWebElement btnBurgerMenu { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Reset App State')]")]
        private IWebElement btnResetAppState { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='shopping_cart_badge']")]
        private IWebElement cartBadge { get; set; }

        [FindsBy(How = How.ClassName, Using = "shopping_cart_link")]
        private IWebElement btnCart { get; set; }

        public bool IsCartEmpty()
        {
            return !driver.FindElements(By.CssSelector(".shopping_cart_badge")).Any();
        }
        public void Verify_ResetAppState()
        {
            Assert.IsTrue(IsCartEmpty(), "Cart is not empty on the inventory page.");
        }
        public void AddItemToCart()
        {
            btnAddToCart_Item.Click();
            string actual = lnkCartWith_OneItem.Text;
            string expected = "1";
            Assert.That(expected, Is.EqualTo(actual));
            btnCart.Click();

        }



    }
}
