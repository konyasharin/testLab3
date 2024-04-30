using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumProject.pages
{
    public class BasketPage
    {
        private readonly IWebDriver _driver;
        public BasketPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ReadOnlyCollection<IWebElement> GetProducts()
        {
            return _driver.FindElements(By.ClassName("cart-item"));
        }
        
        public void DeleteProduct(IWebElement product)
        {
            product.FindElement(By.ClassName("cart-item__delete-wrapper")).Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("overlay")));
        }
    }
}