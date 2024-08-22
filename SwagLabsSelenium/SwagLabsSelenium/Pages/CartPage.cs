using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsSelenium.Pages
{
    public class CartPage
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='cart_item_label']/a/div")]
        public IWebElement txtCartItem { get; set; }

        [FindsBy(How = How.Id, Using = "checkout")]
        private IWebElement btnCheckout { get; set; }

        public void Checkout()
        {
            btnCheckout.Click();
        }
    }
}
