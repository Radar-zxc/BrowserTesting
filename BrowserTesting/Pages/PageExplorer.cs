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
using System.Collections;
using System.Linq;

namespace BrowserTesting.Pages
{
    class PageExplorer : BasePage
    {
        public string newUrl;
        //private IReadOnlyCollection<string> tabsList;
        private IEnumerable<string> tabsList;
        private List<string> tabsDescriptorList = new List<string>();
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
        public string GetItemPageUrl(string itemName)
        {
            string pageUrl = Driver.FindElement
                (By.XPath($"//div[@class='item-box']//h2[@class='product-title']//a[normalize-space(text())='{itemName}']")).GetAttribute("href");
            return newUrl = pageUrl;
        }
        public void OpenPageInNewTab(string pageName)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{pageName}']";
            By newPage = By.XPath(path);
            if (tabsList == null)
            {
                AddTabInDescriptorList();
            }
            OpenPageRef(newPage);
            RefreshTabsList();
            Driver.SwitchTo().Window(tabsList.Last());
            OpenPage(pageName);
            AddTabInDescriptorList();
        }
        private void AddTabInDescriptorList()
        {
            tabsDescriptorList.Add(Driver.CurrentWindowHandle);
        }
        private void RefreshTabsList()
        {
            tabsList = Driver.WindowHandles;
        }
        public void GoToTab(int tabNubmer)
        {
            RefreshTabsList();
            Assert.IsTrue(tabNubmer < tabsDescriptorList.Count && tabNubmer >= 0,
                "Попытка выхода за границу массива");
            foreach (string tab in tabsList)
            {
                if (tab == (string)tabsDescriptorList[tabNubmer])
                {
                    Driver.SwitchTo().Window(tab);
                    break;
                }
            }
        }
    }
}
