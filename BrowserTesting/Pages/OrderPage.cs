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
        //public string itemName; //= "Black & White Diamond Heart";
        public OrderPage (IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        private By itemPathPrice;///= By.XPath("//a[text()='Black & White Diamond Heart']/../..//span[@class='product-unit-price']");
        private By itemAddButton = By.XPath("//div[@class='center-2']//input[@type='button']");
        private string itemPageUrl = "http://demowebshop.tricentis.com/black-white-diamond-heart";
        private By itemCountField = By.XPath("//div[@class='center-2']//input [@class='qty-input']");
       // private int itemCount;

        public void setName (string itemName)
        {
            this.itemName = itemName;
        }
        public void setCount(int count)
        {
            itemCount = count;
        }
        public void addItems()
        {
            var add = Driver.FindElement(itemCountField);
            add.Clear();
            add.SendKeys(itemCount.ToString());
            add.SendKeys(Keys.Enter);
        }
        public void setPathPrice()
        {
            string path = "//a[text()='" + itemName + "']/../..//span[@class='product-unit-price']";
            itemPathPrice = By.XPath(path);
        }
        public double getItemPrice ()
        {
            double itemPrice = double.Parse(Driver.FindElement(itemPathPrice).Text);
            return itemPrice;
        }
        public bool checkPageAndUrlContent(string Url)
        {
            return (Url == itemPageUrl);
        }
        public int getItemCount()
        {
            return itemCount;
        }
    }
}
