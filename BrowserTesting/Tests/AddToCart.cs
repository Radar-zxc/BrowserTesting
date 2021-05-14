using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    [TestFixture]
    class AddToCart:TestBase
    {
        public override void DriverSetUp()
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

        [Test, Description("Add jewelry to cart"), Order(0)]
        public void AddJewelry_MultiplyInCart()
        {
            OpenPage("jewelry");
            CheckItemNames("Black & White Diamond Heart");
            AddMoreItems(50);
            CheckManyItemPrice(50);
        }
        [Test, Description("Clear cart - add many items in item page"), Order(1)]
        public void AddJewelry_MultiplyInItemPage()
        {
            string item = "Black & White Diamond Heart";
            cart.RemoveItem(item);
            cart.UpdateCart();
            cart.CheckEmptyCart();
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(item);
            order.CreatePage();
            order.ChangeItemCount(60);
            order.AddItemToCart();
            explorer.OpenCart();
            explorer.CheckCartTravel(CartPage.cartUrl);
            cart.CheckCartItem(item);
            cart.CheckPrice(item);
            cart.RemoveItem(item);
            cart.UpdateCart();
            cart.CheckEmptyCart();
        }
    }
}
