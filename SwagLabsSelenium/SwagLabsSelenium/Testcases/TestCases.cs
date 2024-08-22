using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SwagLabsSelenium.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsSelenium.Testcases
{
    public class TestCases
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private InventoryPage inventoryPage;
        private CartPage cartPage;
        private CheckoutPage checkoutPage;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            //IWebDriver wait= new WebDriver(driver);
            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);
        }
        [Test, Description("Test case ID: TC01 - User login with valid credentials.")]
        public void TestUser_LoginWithValidCredentials()
        {
            loginPage.LoginApp("standard_user", "secret_sauce");
            Assert.IsTrue(driver.Url.Contains("inventory.html"));
            Assert.IsTrue(loginPage.titleProducts.Displayed);
            Assert.IsTrue(loginPage.VerifyProducts());
        }

        [Test, Description("Test case ID: TC02 - User adding item to the cart")]
        public void TestUser_AddItemToCart()
        {
            loginPage.LoginApp("standard_user", "secret_sauce");
            inventoryPage.AddItemToCart();
            Assert.IsTrue(driver.Url.Contains("cart.html"));
            string Actual_Item = cartPage.txtCartItem.Text;
            string Expected_Item = "Sauce Labs Backpack";
            Assert.That(Expected_Item, Is.EqualTo(Actual_Item));

        }

        [Test, Description("Test case ID: TC03 - User placing order")]
        public void TestUser_PlaceOrder()
        {
            loginPage.LoginApp("standard_user", "secret_sauce");
            inventoryPage.Verify_ResetAppState();   
            inventoryPage.AddItemToCart();
            cartPage.Checkout();
            Assert.IsTrue(driver.Url.Contains("checkout-step-one.html"));
            checkoutPage.PlaceOrder("bob", "steve", "12345");
            Assert.IsTrue(driver.Url.Contains("checkout-step-two"));
            checkoutPage.btnFinish.Click();
            Assert.IsTrue(driver.Url.Contains("checkout-complete"));
            String ActualText = checkoutPage.msgSuccess.Text;
            String ExpectedText = "Thank you for your order!";
            Assert.AreEqual(ActualText, ExpectedText);


        }
        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}

    
