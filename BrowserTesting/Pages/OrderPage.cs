using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
namespace BrowserTesting
{
    class OrderPage : BasePage
    {
        public string itemName;
        public int itemCount;
        public OrderPage (IWebDriver Driver):base(Driver)
        {
            SetName("Black & White Diamond Heart");
            SetCount(50);
        }
        public string itemPageUrl = "http://demowebshop.tricentis.com/black-white-diamond-heart";
        public double itemPrice;
        public By itemAddButton;
        public By itemCountField;
        public void SetName (string itemName)
        {
            this.itemName = itemName;
        }
        public void SetCount(int itemCount)
        {
           this.itemCount = itemCount;
        }
        public void SetItemAddButton()
        {
            string path = "//div[@class='center-2']//input[@type='button']";
            itemAddButton = By.XPath(path);
        }
        public void SetItemCountField()
        {
            string path = "//div[@class='center-2']//input [@class='qty-input']";
            itemCountField = By.XPath(path);
        }
        public void SetItemPrice()
        {
            string path = "//span[@itemprop='price']";
            itemPrice = double.Parse(Driver.FindElement(By.XPath(path)).Text);
        }
        public void CreatePage()
        {
            SetItemAddButton();
            SetItemCountField();
            SetItemPrice();
        }
        public bool CheckPageAndUrlContent(string Url)
        {
            return (Url == itemPageUrl);
        }
    }
}
