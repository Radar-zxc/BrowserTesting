﻿using System;
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
            cart.CreatePage();
            explorer.RemoveCart(cart.itemRemoveButton);
            cart.CheckEmptyCart();
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(order.itemName);
            order.CheckPageAndUrlContent(Driver.Url);
            order.CreatePage();
            explorer.ChangeCount(order.itemCountField, 60);
            explorer.AddItem(order.itemAddButton);
            explorer.OpenCart();
            explorer.CheckCartTravel(cart.cartUrl);
            cart.CreatePage();
            cart.CheckCartItem(order.itemName);
            cart.CheckPrice();
            explorer.RemoveCart(cart.itemRemoveButton);
            cart.CheckEmptyCart();
        }
    }
}
