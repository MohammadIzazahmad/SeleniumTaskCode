using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsSelenium.Pages
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Id, Using = "first-name")]
        private IWebElement txtFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "last-name")]
        private IWebElement txtLastName { get; set; }

        [FindsBy(How = How.Id, Using = "postal-code")]
        private IWebElement txtZipcode { get; set; }

        [FindsBy(How = How.Id, Using = "continue")]
        private IWebElement btnContinue { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".summary_subtotal_label")]
        private IWebElement txtSubtotal { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='summary_tax_label']")]
        private IWebElement txtTax { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='summary_total_label']")]
        private IWebElement txtTotal { get; set; }

        [FindsBy(How = How.Id, Using = "finish")]
        public IWebElement btnFinish { get; set; }

        [FindsBy(How = How.XPath, Using = "//h2[contains(text(),'Thank you for your order')]")]
        public IWebElement msgSuccess { get; set; }

        public void PlaceOrder(string firstName, string lastName, string postalCode)
        {
            txtFirstName.SendKeys(firstName);
            txtLastName.SendKeys(lastName);
            txtZipcode.SendKeys(postalCode);
            btnContinue.Click();
            string subTotalText = txtSubtotal.Text.Replace("Item total: $", "").Trim();
            string taxText = txtTax.Text.Replace("Tax: $", "").Trim();
            string totalText = txtTotal.Text.Replace("Total: $", "").Trim();

            decimal itemTotal = decimal.Parse(subTotalText);
            decimal tax = decimal.Parse(taxText);
            decimal expectedTotal = itemTotal + tax;
            decimal actualTotal = decimal.Parse(totalText);
            Assert.AreEqual(expectedTotal, actualTotal);
            
            


        }
    }
}
