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
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        private CartPage cart;
        private OrderPage order;
        private PageExplorer explorer;
        private string item1;
        private string item2;
        private string item3;
        [OneTimeSetUp]
        public void Prepare()
        {
            order= new OrderPage(Driver);
            cart = new CartPage(Driver);
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
            order.CreatePage();
            for (int i = 0; i < items.Length; i++)
            {
                OpenPage("books");
                explorer.GoToItemPage(items[i]);
                order.ChangeItemCount(1);
                order.AddItemToCart();
            }
            explorer.OpenCart();
            explorer.CheckCartTravel(CartPage.cartUrl);
            for (int i=0; i<items.Length;i++)
            {
                cart.CheckCartItem(items[i]);
                cart.RemoveItem(items[i]);
            }
            cart.UpdateCart();
            cart.CheckEmptyCart();
            explorer.OpenStartPage();
        }
    }
}
