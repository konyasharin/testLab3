using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SeleniumProject.pages
{
    public class MainPage
    {
        private readonly IWebDriver _driver;
        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ReadOnlyCollection<IWebElement> GetNavigationElements()
        {
            return _driver.FindElement(By.ClassName("header-bottom__menu"))
                .FindElements(By.ClassName("header-bottom__url"));
        }

        public int SearchProduct(string productName)
        {
            var searchForm = _driver.FindElement(By.Id("search_form"));
            searchForm.FindElement(By.Id("search-input")).SendKeys(productName);
            searchForm.Submit();
            return _driver.FindElement(By.ClassName("softiq-products-block"))
                .FindElements(By.ClassName("softiq-product")).Count;
        }
    }
}