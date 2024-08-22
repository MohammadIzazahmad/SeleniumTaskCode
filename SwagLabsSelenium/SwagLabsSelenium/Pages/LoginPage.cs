using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SwagLabsSelenium.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement txtLoginId { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement txtPwd { get; set; }

        [FindsBy(How = How.Id, Using = "login-button")]
        private IWebElement btnLogin { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Products')]")]
        public IWebElement titleProducts { get; set; }

        [FindsBy(How = How.ClassName, Using = "inventory_item")]
        private IList<IWebElement> productItems { get; set; }

        public bool VerifyProducts()
        {
            // Checks if there are any product items on the page
            return productItems.Any();
        }
        public void LoginApp(string username, string password)
        {
            txtLoginId.SendKeys(username);
            txtPwd.SendKeys(password);
            btnLogin.Click();
            Assert.IsTrue(titleProducts.Displayed);
        }
    }
}
