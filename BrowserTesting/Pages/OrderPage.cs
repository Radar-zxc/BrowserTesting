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
        public string itemName;
        public int itemCount;
        public OrderPage (IWebDriver Driver/*,string itemName,int itemCount*/):base(Driver)
        {
            //this.Driver = Driver;
            setName("Black & White Diamond Heart");
            setCount(50);
            //setPathPrice();
        }

        /* private By itemPathPrice;///= By.XPath("//a[text()='Black & White Diamond Heart']/../..//span[@class='product-unit-price']");
         private By itemAddButton = By.XPath("//div[@class='center-2']//input[@type='button']");
         private string itemPageUrl = "http://demowebshop.tricentis.com/black-white-diamond-heart";
         private By itemCountField = By.XPath("//div[@class='center-2']//input [@class='qty-input']");
        */
        // private int itemCount;
        public double itemPrice;
        public IWebElement itemAddButton;
        public IWebElement itemCountField;
        public void setName (string itemName)
        {
            this.itemName = itemName;
        }
        public void setCount(int itemCount)
        {
           this.itemCount = itemCount;
        }
        /*public void addItems()
        {
            var add = Driver.FindElement(itemCountField);
            add.Clear();
            add.SendKeys(itemCount.ToString());
            add.SendKeys(Keys.Enter);
        }*/
        /*public void setPathPrice()
        {
            string path = "//a[text()='" + itemName + "']/../..//span[@class='product-unit-price']";
            itemPathPrice = By.XPath(path);
        }*/
        /*public double getItemPrice ()
        {
            double itemPrice = double.Parse(Driver.FindElement(itemPathPrice).Text);
            return itemPrice;
        }*/
        public void SetItemAddButton()
        {
            string path = "//div[@class='center-2']//input[@type='button']";
            itemAddButton = Driver.FindElement(By.XPath(path));
        }
        public void SetItemCountField()
        {
            string path = "//div[@class='center-2']//input [@class='qty-input']";
            itemCountField = Driver.FindElement(By.XPath(path));
        }
        public void SetItemPrice()
        {
            //string path = $"//a[text()='{itemName}']/../..//span[@class='product-unit-price']";
            string path = "//span[@itemprop='price']";
            itemPrice = double.Parse(Driver.FindElement(By.XPath(path)).Text);
        }
        public void CreatePage()
        {
            SetItemAddButton();
            SetItemCountField();
            SetItemPrice();
        }


       /*public bool checkPageAndUrlContent(string Url)
        {
            return (Url == itemPageUrl);
        }*/

        /*public int getItemCount()
        {
            return itemCount;
        }*/
    }
}
