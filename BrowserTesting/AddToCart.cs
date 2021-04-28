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
            //cart = new CartPage(Driver, itemName, itemCount);
            //string itemName = "Black & White Diamond Heart";
            //int itemCount = 50;
            cart = new CartPage(Driver);
            order = new OrderPage(Driver);
            explorer = new PageExplorer(Driver);
            //cart.itemName = itemName;
            //cart.itemCount = itemCount;
            //order.itemName = itemName;
            //order.itemCount = itemCount;

        }

        [Test, Description("Add jewelry to cart"), Order(0)]
        public void AddJewelry_MultiplyInCart()
        {
            OpenPage("jewelry");
            CheckItemNames("Black & White Diamond Heart");
            AddMoreItems(50);
            CheckManyItemPrice(50);
           
        }
        [Test, Order(1)]
        public void AddJewelry_MultiplyInItemPage()
        {
            cart.CreatePage();
            cart.RemoveCart(cart.itemRemoveButton);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(order.itemName);
            order.CreatePage();
            explorer.ChangeCount(order.itemCountField, 60);
            explorer.AddItem(order.itemAddButton);
            explorer.OpenCart();
            //cart.CreatePage();

            //string itemName = "Black & White Diamond Heart";
            //int itemCount = 50;
            /* cart.itemName = itemName;
             cart.itemCount = itemCount;
             order.itemName = itemName;
             order.itemCount = itemCount;*/

            /*
            cart.ChangeCountInCart("Black & White Diamond Heart", 20);
            cart.RemoveCart(cart.RemoveCart());
            //cart.RemoveCart();
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage("Black & White Diamond Heart");
            order.addItems();
            order.checkPageAndUrlContent(Driver.Url);
            explorer.OpenCart();

            */
            //RemoveCart("Black & White Diamond Heart", 50);
            //OpenPage("jewelry");
            //string s = GoToJewelryItemPage("Black & White Diamond Heart");
            ///CheckItemNamesInItemPage(s);
            //AddItemFromPage();
            //CartPage cart = new CartPage(Driver,itemName,itemCount);
            //cart.setFields(itemName,itemCount);
            // removeCart(cart.itemRemoveButton);
            //OpenPage("jewelry");
            //GoToJewelryItemPage(cart.itemName);
            //AddItemFromPage();
            //cart.removeCart();
        }
    }
}
