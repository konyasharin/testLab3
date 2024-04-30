using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SeleniumProject.pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SetMinPriceFilter(int min)
        {
            _driver.FindElement(By.ClassName("price-filter-container")).FindElement(By.ClassName("price-filter__item")).Click();
            _driver.FindElement(By.Id("price_min")).SendKeys("15000");
            _driver.FindElement(By.ClassName("price-filter")).FindElement(By.TagName("form")).Submit();
        }

        public ReadOnlyCollection<IWebElement> GetProducts()
        {
            return _driver.FindElement(By.ClassName("products-items-list")).FindElements(By.ClassName("product-list"));
        }

        public int GetProductPrice(IWebElement product)
        {
            return int.Parse(product.FindElement(By.ClassName("product-card-list__cash")).Text.Replace(" ", "")
                .Replace("\u20bd", ""));
        }

        public string AddToBasket(IWebElement product)
        {
            var addButton = product.FindElement(By.ClassName("add_to_cart"));
            addButton.Click();
            return addButton.Text;
        }
    }
}