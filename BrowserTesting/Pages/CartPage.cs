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
    class CartPage:BasePage
    {
        //private IWebDriver Driver;
        public string cartUrl = "http://demowebshop.tricentis.com/cart";
        //public string itemName;
        //public int itemCount;
        public CartPage(IWebDriver Driver )
        {
            this.Driver = Driver;
            //setFields(name, co);
        }
        public By itemRemoveButton;
        private string itemRefNameInCart;
        private By itemPathPrice;
        private By itemPathTotalPrice; 
        
        /*public void setItemName(string itemName)
        {
            this.itemName = itemName;
        }*/
        public void setRemoveButton()
        {
            string pathRemoveButton = "//tr[@class='cart-item-row']//a[normalize-space(text()='" +
            itemName + "')]//..//..//input[@type='checkbox']";
            itemRemoveButton = By.XPath(pathRemoveButton);
        }
        public void setPrice()
        {
            //string pathItemPrice = "//a[text()='Black & White Diamond Heart']/../..//span[@class='product-unit-price']";
            string pathItemPrice = "//a[text()='" + itemName + "']/../..//span[@class='product-unit-price']";
            itemPathPrice = By.XPath(pathItemPrice);
        }
        public void setTotalPrice ()
        {
            //string pathItemTotalPrice = "//a[text()='Black & White Diamond Heart']/../..//span[@class='product-subtotal']";
            string pathItemTotalPrice = "//a[text()='" + itemName + "']/../..//span[@class='product-subtotal']";
            itemPathTotalPrice = By.XPath(pathItemTotalPrice);
        }
        public void setRefNameInCart()
        {
            //string pathItemName = "//tr[@class='cart-item-row']//td[@class='product']//a[text()='Black & White Diamond Heart']";
            string pathItemName = $"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']";
            itemRefNameInCart = Driver.FindElement(By.XPath(pathItemName)).GetAttribute("href");
            
        }
        public double getPrice()
        {
            double itemPrice = double.Parse(Driver.FindElement(itemPathPrice).Text);
            return itemPrice;
        }
        public double getTotalPrice()
        {
            double itemTotalPrice = double.Parse(Driver.FindElement(itemPathTotalPrice).Text);
            return itemTotalPrice;
        }
        public bool checkPrice ()
        {
            return ((getPrice() * itemCount) == getTotalPrice());
        }
       /* public void removeCart()
        {
            //setRemoveButton();
            var remove = Driver.FindElement(itemRemoveButton);
            remove.Click();
        }*/
        public void checkEmptyCart()
        {
            var findMessage = Driver.FindElement(By.XPath("//div[@class='page-body']//div[normalize-space(text()='Your Shopping Cart is empty!')])"));
            Assert.IsTrue(findMessage.Displayed, "Корзина не очищена");
        }
        public CartPage goToCart()
        {
            var cartRef = Driver.FindElement(By.XPath("//div[@class='header-links-wrapper']//a[@class='ico-cart']//span[@class='cart-label']"));
            cartRef.Click();
            return new CartPage(Driver);
        }
        public void checkCartUrl()
        {
            Assert.AreEqual(Driver.Url, cartUrl, "Текстовая ссылка перенаправляет на неверный адрес");
        }
        /*public void setItemCount(int count)
        {
            itemCount = count;
        }*/
        public void setFields(string itemName,int count)
        {
            //setItemName(itemName);
            setRemoveButton();
            setPrice();
            setTotalPrice();
            //setItemCount(count);
            //setRefNameInCart();
        }


    }
}
