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
namespace BrowserTesting.Pages
{
    class PageExplorer:BasePage
    {
        public PageExplorer(IWebDriver Driver):base(Driver)
        {
        }
        public void OpenPage(string pageName)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{pageName}']";
            var find = Driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenPageWithList(string pageName, string pageElement)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{ pageName}']";
            var find = Driver.FindElement(By.XPath(path));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(find).Build().Perform();
            path = $"//ul[@class='top-menu']//ul[@class='sublist firstLevel active']//a[@href='/{ pageElement}']";
            find = Driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenStartPage()
        {
            string path = "//a[normalize-space(text()='Tricentis Demo Web Shop')]//img[@title='']";
            var find = Driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenCart()
        {
            var cart = Driver.FindElement(By.XPath("//div[@class='header-links-wrapper']//a[@class='ico-cart']//span[@class='cart-label']"));
            cart.Click();
        }

        public void GoToItemPage(string itemName)
        {
            string pathItem = $"//div[@class='page-body']//div[@class='item-box']//a[text()='{ itemName}']";
            var item = Driver.FindElement(By.XPath(pathItem));
            item.Click();
        }
        public void CheckCartTravel(string url)
        {
            Assert.AreEqual(Driver.Url , url, "Осуществлен неверный переход при попытке перейти в корзину");
        }
    }
}
