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
    class SwitchAndUpdatePage : TestBase
    {
        private PageExplorer explorer;
        private OrderPage order;
        private CartPage cart;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            order = new OrderPage(Driver);
            cart = new CartPage(Driver);
        }
        [Test, Description("Сhecking the synchronization of tabs"), Order(0)]
        public void СheckingSynchronization()
        {
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage("Black & White Diamond Heart");
            order.CreatePage();
            order.AddItemToCart();
            explorer.OpenCart();
            cart.CheckCartItem("Black & White Diamond Heart");
            explorer.OpenPageInNewTab("books");
            explorer.GoToItemPage("Computing and Internet");
            order.AddItemToCart();
            explorer.GoToTab(0);
            cart.CheckCartItemAbsence("Computing and Internet");
            cart.UpdateCart();
            cart.CheckCartItem("Black & White Diamond Heart");
            cart.CheckCartItem("Computing and Internet");

            /*var b = Driver.FindElement(By.XPath("//a[@href='/books']"));
            Actions newTab = new Actions(Driver);
            newTab
                .KeyDown(Keys.Control)
                .KeyDown(Keys.Shift)
                .Click(b).KeyUp(Keys.Control).KeyUp(Keys.Shift)
                .Build()
                .Perform();*/
            explorer.GoToTab(0);
        }
    }
}
