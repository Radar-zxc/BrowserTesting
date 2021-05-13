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
    [TestFixture("Computing and Internet", "Fiction", "Health Book")]
    class AddManyItemsToCart:TestBase
    {
        override public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        private CartPage[] cart =new CartPage[3];
        private OrderPage[] order = new OrderPage[3];
        private PageExplorer explorer;
        private string item1;
        private string item2;
        private string item3;
        [OneTimeSetUp]
        public void Prepare()
        {
            for (int i = 0; i < 3; i++)
            {
                cart[i] = new CartPage(Driver);
                order[i] = new OrderPage(Driver);
            }
            explorer = new PageExplorer(Driver);
        }
        public AddManyItemsToCart(string item1, string item2, string item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }
        [Test, Description("Add 3 books to cart, check cart content"), Order(0)]
        public void AddDifferentBooks_CheckRemove()
        {
            string[] items = new string[] { item1, item2, item3 };

            for (int i = 0; i < items.Length; i++)
            {
                OpenPage("books");
                explorer.GoToItemPage(items[i]);
                order[i].CheckPageAndUrlContent(Driver.Url);
                order[i].CreatePage(items[i]);
                order[i].ChangeItemCount(1);
                order[i].AddItemToCart(items[i]);
            }
            explorer.OpenCart();
            explorer.CheckCartTravel(CartPage.cartUrl);
            for (int i=0; i<items.Length;i++)
            {
                cart[i].CreateRow(items[i]);
                cart[i].CheckCartItem(order[i].itemName);
                cart[i].CheckPrice();
                cart[i].RemoveItem(items[i]);
            }
            cart[0].UpdateCart();
            explorer.OpenStartPage();
        }
    }
}
