using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Enums;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace BrowserTesting
{
    public class TestBase
    {
        protected IWebDriver Driver;
        [OneTimeSetUp]
        public void OpenBrowserWithJson()
        {
            var jsonText = File.ReadAllText("Appsettings.json");
            var convertedBrowser = JsonConvert.DeserializeObject<Browser>(jsonText);
            Browsers browser = (Browsers)Enum.Parse(typeof(Browsers),convertedBrowser.name);
            switch (browser){
                case Browsers.Firefox:
                    Driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    Driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
                case Browsers.IE:
                    Driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                    break;
                case Browsers.Edge:
                    Driver = new OpenQA.Selenium.Edge.EdgeDriver();
                    break;
                default:
                    throw new Exception("Неудалось определить тип браузера");
            }
            Driver.Manage().Window.Maximize();
        }
        [OneTimeSetUp]
        virtual public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("https://www.google.ru/");
        }
        [OneTimeSetUp]
        public void ChangeCultureToUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        public void UrlVerify(string necessaryUrl)
        {
            string pageUrl = Driver.Url;
            Assert.IsTrue(pageUrl.Contains(necessaryUrl),
                "Неверный Url после перехода на вкладку");
        }
        public void ContentVerify(string key)
        {
            string xpathCheck = "//div[@class='page-title']//h1[text()='" + key + "']";
            var check = Driver.FindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        public void OpenPage(string pageName )
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + pageName + "']";
            var find = Driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenPageWithList(string pageName,string pageElement)
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + pageName + "']";
            var find = Driver.FindElement(By.XPath(path));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(find).Build().Perform();
            path = "//ul[@class='top-menu']//ul[@class='sublist firstLevel active']//a[@href='/" + pageElement + "']";
            find = Driver.FindElement(By.XPath(path));
            find.Click();
        }
        public string FindAddJewelryItem(string itemName)
        {
            string pathName = "//div[@class='page-body']//div[@class='item-box']//a[text()='" + itemName + "']";
            var findName = Driver.FindElement(By.XPath(pathName)).GetAttribute("href");
            string pathElement = "//div[@class='page-body']//div[@class='item-box']//div[@data-productid='14']//h2[normalize-space(a/text())='"
                + itemName + "']/..//input[@type='button']";
            var findElement = Driver.FindElement(By.XPath(pathElement));
            findElement.Click();
            return findName;
        }
        public string CheckCartItem()
        {
            string pathCart = "//div[@class='header-links-wrapper']//a[@class='ico-cart']//span[@class='cart-label']";
            var findCart = Driver.FindElement(By.XPath(pathCart));
            Actions move = new Actions(Driver);
            move.MoveToElement(findCart).Perform();
            findCart.Click();
            string pathItemName = "//tr[@class='cart-item-row']//td[@class='product']//a[text()='Black & White Diamond Heart']";
            var findName = Driver.FindElement(By.XPath(pathItemName)).GetAttribute("href");
            return findName;
        }
        public void AddMoreItems(int count)
        {
            string pathItemCount = "//a[text()='Black & White Diamond Heart']/../..//input[@class='qty-input']";
            var addItems = Driver.FindElement(By.XPath(pathItemCount));
            addItems.Clear();
            addItems.SendKeys(count.ToString());
            addItems.SendKeys(Keys.Enter);
        }
        public void CheckManyItemPrice(int count)
        {
            string pathItemTotalPrice = "//a[text()='Black & White Diamond Heart']/../..//span[@class='product-subtotal']";
            string pathItemPrice = "//a[text()='Black & White Diamond Heart']/../..//span[@class='product-unit-price']";
            var itemPrice = double.Parse(Driver.FindElement(By.XPath(pathItemPrice)).Text);
            var itemTotalPrice =double.Parse( Driver.FindElement(By.XPath(pathItemTotalPrice)).Text);
            var check = itemPrice * count;
            Assert.IsTrue(check == itemTotalPrice, "Неверное вычисление итоговой суммы к оплате за товар");
        }
        public void CheckItemNames(string itemName)
        {
            Assert.IsTrue(FindAddJewelryItem(itemName) == CheckCartItem(), 
                "Выбранный для добавления товар и товар ,добавленный в корзину, не совпадают");
        }
        public void RemoveCart(string itemName,int count)
        {
            string pathRemoveButton = "//tr[@class='cart-item-row']//a[normalize-space(text()='" + 
                itemName + "')]//..//..//input[@value='" + count + "']//..//..//input[@type='checkbox']";
            var removeButton = Driver.FindElement(By.XPath(pathRemoveButton));
            removeButton.Click();
            removeButton.SendKeys(Keys.Enter);
        }
        public string GoToJewelryItemPage(string itemName)
        {
            string pathItem = "//div[@class='page-body']//div[@class='item-box']//a[text()='" + itemName + "']";
            var item = Driver.FindElement(By.XPath(pathItem));
            var findName = Driver.FindElement(By.XPath(pathItem)).GetAttribute("href"); //это url по нему потом сравнить открывшуюся страницу и нажимаемую
            item.Click();
            return findName;
        }
        public void CheckItemNamesInItemPage(string itemName)
        {
            Assert.IsTrue(itemName == Driver.Url,
               "Выбранный товар и страница с информацией о нем не совпадают");
        }
        public void AddItemFromPage()
        {
            string pathMultiplyItem = "//div[@class='center-2']//input [@class='qty-input']";
            var add = Driver.FindElement(By.XPath(pathMultiplyItem));
            add.Clear();
            add.SendKeys("50");
            add.SendKeys(Keys.Enter);
        }

        [OneTimeTearDown]
        public void ChangeCultureToRU()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
        }
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
