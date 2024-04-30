using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumProject.pages;

namespace SeleniumProject
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _driver;
 
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=1920,1080");
            _driver = new ChromeDriver(options);
        }
 
        [Test]
        public void TestNavigate()
        {
            MainPage mainPage = new MainPage(_driver);
            _driver.Navigate().GoToUrl("https://tehnomaks.ru");
            var findElements = mainPage.GetNavigationElements();
            for (int i = 0; i < findElements.Count; i++)
            {
                string newUrl = findElements[i].GetAttribute("href");
                if (findElements[i].Displayed)
                {
                    findElements[i].Click();
                    Assert.AreEqual(_driver.Url, newUrl, "Ссылка не работает");
                    _driver.Navigate().Back();
                    findElements = mainPage.GetNavigationElements();   
                }
            }
        }
        
        [Test]
        [TestCase("RTX 4060")]
        [TestCase("GTX 1650")]
        [TestCase("RTX 3070 TI")]
        public void TestSearch(string productName)
        {
            MainPage mainPage = new MainPage(_driver);
            _driver.Navigate().GoToUrl("https://tehnomaks.ru");
            int count = mainPage.SearchProduct(productName);
            Thread.Sleep(5000);
            Assert.AreNotEqual(_driver.Url, "https://tehnomaks.ru");
            Assert.NotZero(count);
        }

        [Test]
        public void TestFilter()
        {
            ProductsPage productsPage = new ProductsPage(_driver);
            _driver.Navigate().GoToUrl("https://tehnomaks.ru/catalog/section/noutbuki");
            productsPage.SetMinPriceFilter(15000);
            foreach (var product in productsPage.GetProducts())
            {
                Assert.GreaterOrEqual(productsPage.GetProductPrice(product), 15000);
            }
        }
        
        [Test]
        public void TestAddToBasket()
        {
            ProductsPage productsPage = new ProductsPage(_driver);
            BasketPage basketPage = new BasketPage(_driver);
            _driver.Navigate().GoToUrl("https://tehnomaks.ru/catalog/section/noutbuki");
            string newText = productsPage.AddToBasket(productsPage.GetProducts()[0]);
            Assert.AreEqual(newText, "В корзине");
            _driver.Navigate().GoToUrl("https://tehnomaks.ru/basket");
            Assert.NotZero(basketPage.GetProducts().Count);
        }

        [Test]
        public void TestDeleteFromBasket()
        {
            ProductsPage productsPage = new ProductsPage(_driver);
            BasketPage basketPage = new BasketPage(_driver);
            _driver.Navigate().GoToUrl("https://tehnomaks.ru/catalog/section/noutbuki");
            string newText = productsPage.AddToBasket(productsPage.GetProducts()[0]);
            Assert.AreEqual(newText, "В корзине");
            _driver.Navigate().GoToUrl("https://tehnomaks.ru/basket");
            Assert.NotZero(basketPage.GetProducts().Count);
            basketPage.DeleteProduct(basketPage.GetProducts()[0]);
            Assert.Zero(basketPage.GetProducts().Count);
        }
 
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}