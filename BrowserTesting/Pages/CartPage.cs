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
    class CartPage : BasePage
    {
        public static string cartUrl = "http://demowebshop.tricentis.com/cart";
        public string itemName;
        public int itemCount;
        public CartPage(IWebDriver Driver) : base(Driver)
        {
            //SetItemName("Black & White Diamond Heart");
            SetItemCount(1);
        }
        public By itemRemoveButton;
        public string itemRefNameInCart;
        public double itemPrice;
        public double itemTotalPrice;
        public By itemCountField;
        public By updateButton= By.XPath("//input[@class='button-2 update-cart-button']");
        
        public void SetItemName(string itemName)
        {
            this.itemName = itemName;
        }
        public By SetRemoveButton(string itemName)
        {
            string path = $"//tr[@class='cart-item-row']//a[normalize-space(text())='{itemName}']//..//..//input[@type='checkbox']";
            return itemRemoveButton = By.XPath(path);
        }
        public void SetPrice(string itemName)
        {
            string pathItemPrice = $"//a[text()='{itemName}']/../..//span[@class='product-unit-price']";
            itemPrice = double.Parse(Driver.FindElement(By.XPath(pathItemPrice)).Text);
        }
        public void SetTotalPrice(string itemName)
        {
            string pathItemTotalPrice = $"//a[text()='{itemName}']/../..//span[@class='product-subtotal']";
            itemTotalPrice = double.Parse(Driver.FindElement(By.XPath(pathItemTotalPrice)).Text);
        }
        public void SetCountField(string itemName)
        {
            string pathItemCount = $"//a[text()='{itemName}']/../..//input[@class='qty-input']";
            itemCountField = By.XPath(pathItemCount);
        }
        public void SetRefNameInCart(string itemName)
        {
            string pathItemName = $"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']";
            itemRefNameInCart = Driver.FindElement(By.XPath(pathItemName)).GetAttribute("href");
        }
        public bool CheckPrice ()
        {
            return ((itemPrice * itemCount) == itemTotalPrice);
        }
        public void CheckEmptyCart()
        {
            var findMessage = Driver.FindElement(By.XPath("//div[@class='page-body']//div[normalize-space(text())='Your Shopping Cart is empty!']"));
            Assert.IsTrue(findMessage.Displayed, "Корзина не очищена");
        }
        public void CheckCartItem(string orderItemName)
        {
            Assert.AreEqual(orderItemName, itemName, "Предмет, добавленный в корзину и оказавшийся в корзине не совпадают");
        }
        public void SetItemCount(int count)
        {
            itemCount = count;
        }
        public void ChangeItemCount(string item, int newCount)
        {
            SetCountField(item);
            ChangeCount(itemCountField, newCount);
        }
        public void UpdateCart()
        {
            RefreshCart(updateButton);
        }
        public void RemoveItem(string itemName)
        {
            RemoveCart(SetRemoveButton(itemName));
        }
        public void CreateRow(string itemName)
        {
            SetItemName(itemName);
            SetRemoveButton(itemName);
            SetCountField(itemName);
            SetTotalPrice(itemName);
            SetPrice(itemName);
        }
    }
}
