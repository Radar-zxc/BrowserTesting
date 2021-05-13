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
        public OrderPage (IWebDriver Driver):base(Driver)
        {

        }
        public By itemPrice;
        public By itemAddButton;
        public By itemCountField;
        public void SetName (string itemName)
        {
            this.itemName = itemName;
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
        public double SetItemValuePrice()
        {
            string path = "//span[@itemprop='price']";
            double price = double.Parse(Driver.FindElement(By.XPath(path)).Text);
            return price;
        }
        public void SetItemPrice()
        {
            itemPrice = By.XPath("//span[@itemprop='price']");
        }
        public void ChangeItemCount(int count)
        {
            ChangeCount(itemCountField, count);
        }
        public void AddItemToCart()
        {
            AddItem(itemAddButton);
        }
        public void CreatePage()
        {
            SetItemAddButton();
            SetItemCountField();
            SetItemPrice();
        }
    }
}
