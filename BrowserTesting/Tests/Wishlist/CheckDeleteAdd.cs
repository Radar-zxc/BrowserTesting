﻿using NUnit.Framework;
using BrowserTesting.Pages;
using System;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading;
using AventStack.ExtentReports.Reporter;

namespace BrowserTesting.Tests.Wishlist
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckDeleteAdd :TestBase
    {
        protected PageExplorer explorer;
        protected OrderPage order;
        protected CartPage cart;
        protected WishlistPage wishlist;
        protected string itemName;
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
            wishlist = new WishlistPage(Driver);
        }
        public CheckDeleteAdd(string item1)
        {
            itemName = item1;
        }
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = (typeof(WishlistTesting).Name).ToString();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\" + $"{className} {DateTime.Now.Date.ToShortDateString()}.html";
            htmlReporter = new ExtentV3HtmlReporter(reportPath);
            //htmlReporter = new ExtentV3HtmlReporter
            //  (@"C:\Users\Family\Source\Repos\BrowserTesting\BrowserTesting\Reports\zxc.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [AutomatedTest(7)][MethodImpl(MethodImplOptions.Synchronized)]
        [Test, Description("Checking the simultaneous deletion and addition of an item to the cart from the Wishlist page via the CheckBox"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.RemoveItem(itemName)
                .AddToCart_CheckBox_On(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
            explorer.OpenCart();
            cart.CheckCartItem(itemName);
            cart.RemoveAllItems();
            cart.UpdateCart();
        }
    }
}
