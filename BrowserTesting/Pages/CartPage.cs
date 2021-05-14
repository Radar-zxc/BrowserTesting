using System;
using System.Collections.Generic;
using System.Collections;
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
        public CartPage(IWebDriver Driver) : base(Driver)
        {

        }
        public By updateButton = By.XPath("//input[@class='button-2 update-cart-button']");

        public By SetRemoveButton(string itemName)
        {
            string path = $"//tr[@class='cart-item-row']//a[normalize-space(text())='{itemName}']//..//..//input[@type='checkbox']";
            By itemRemoveButton = By.XPath(path);
            return itemRemoveButton ;
        }
        public double GetPrice(string itemName)
        {
            string pathItemPrice = $"//a[text()='{itemName}']/../..//span[@class='product-unit-price']";
            double itemPrice = double.Parse(Driver.FindElement(By.XPath(pathItemPrice)).Text);
            return itemPrice;
        }
        public double GetTotalPrice(string itemName)
        {
            string pathItemTotalPrice = $"//a[text()='{itemName}']/../..//span[@class='product-subtotal']";
            double itemTotalPrice = double.Parse(Driver.FindElement(By.XPath(pathItemTotalPrice)).Text);
            return itemTotalPrice;
        }
        public By SetCountField(string itemName)
        {
            string pathItemCount = $"//a[text()='{itemName}']/../..//input[@class='qty-input']";
            By itemCountField = By.XPath(pathItemCount);
            return itemCountField;
        }
        public string SetRefNameInCart(string itemName)
        {
            string pathItemName = $"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']";
            string itemRefNameInCart = Driver.FindElement(By.XPath(pathItemName)).GetAttribute("href");
            return itemRefNameInCart;
        }
        public void CheckPrice(string itemName)
        {
            Assert.AreEqual(GetTotalPrice(itemName), GetPrice(itemName) * GetItemCount(itemName), "Вычисление итоговой суммы произведено неверно");
        }
        public void CheckEmptyCart()
        {
            string path = "//div[@class='page-body']//div[normalize-space(text())='Your Shopping Cart is empty!']";
            Assert.IsTrue(Driver.FindElement(By.XPath(path)).Displayed, "Корзина не очищена");
        }
        public void CheckCartItem(string itemName)
        {
            var findItem = Driver.FindElement(By.XPath($"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']"));
            Assert.IsTrue(findItem.Displayed, $"Предмет {itemName} отсутствует в корзине ");
        }
        public int GetItemCount(string itemName)
        {
            int itemCount = Int32.Parse(Driver.FindElement(SetCountField(itemName)).GetAttribute("value"));
            return itemCount;
        }
        public void ChangeItemCount(string item, int newCount)
        {
            ChangeCount(SetCountField(item), newCount);
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

        }
    }
}
