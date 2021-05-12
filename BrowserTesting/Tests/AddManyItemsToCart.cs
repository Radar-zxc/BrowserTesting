using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting.Tests
{
    class AddManyItemsToCart:TestBase
    {
        override public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        private CartPage cart;
        private OrderPage order;
        private PageExplorer explorer;
        [OneTimeSetUp]
        public void Prepare()
        {
            cart = new CartPage(Driver);
            order = new OrderPage(Driver);
            explorer = new PageExplorer(Driver);
        }
        [Test, Description("Add 3 books to cart, check cart content "), Order(0)]
        public void AddBooks_CheckRemove()
        {
            string[] items = new string[] { item1, item2, item3 };

            for (int i = 0; i < items.Length; i++)
            {
                OpenPage(items[i]);
                explorer.GoToItemPage(items[i]);
                order.CheckPageAndUrlContent(Driver.Url);
                order.CreatePage(items[i]);
                order.ChangeItemCount(1);
                order.AddItemToCart(items[i]);
            }
            explorer.OpenCart();
            explorer.CheckCartTravel(cart.cartUrl);
            foreach (string s in items)
            {
                cart.CreateRow(s);
                cart.CheckCartItem(order.itemName);
                cart.CheckPrice();
            }
        }
    }
}
