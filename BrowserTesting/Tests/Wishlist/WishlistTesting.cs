using NUnit.Framework;
using BrowserTesting.Pages;
using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BrowserTesting
{
    [TestFixture ("Black & White Diamond Heart")]
    class WishlistTesting : TestBase
    {
        protected PageExplorer explorer;
        protected OrderPage order;
        protected CartPage cart;
        protected WishlistPage wishlist;
        protected string itemName;

        public WishlistTesting(string item1)
        {
            itemName = item1;
        }
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
        }/*
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = (typeof(WishlistTesting).Name).ToString();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\"+$"{className} {DateTime.Now.Date.ToShortDateString()}.html";
            htmlReporter = new ExtentV3HtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [TearDown]
        public void GetScreenshotWhenFail()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                string className = (typeof(WishlistTesting).Name).ToString();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string screenshotName = $"{className} {DateTime.Now.Date.ToShortDateString()}.png";
                string screenshotPath = projectPath + "Reports\\" + screenshotName;
                Screenshot file = ((ITakesScreenshot)Driver).GetScreenshot();
                file.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                test.Log(Status.Fail, "Test ended with " + Status.Fail + '\r' + '\n' + TestContext.CurrentContext.Result.StackTrace);
                test.Fail("Fail screenshot: ",
                MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotName).Build());
            }
        }*/
        [AutomatedTest(3)]
        [Test, Description("Add item to wishlist"), Order(0)]
        public void AddToWishlist()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.CheckItem(itemName);
        }
        [AutomatedTest(6)]
        [Test, Description("Check item price in wishlist"), Order(1)]
        public void CalculatePrice()
        {  
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            Assert.Ignore("Check ignore in report");
            wishlist.CheckPrice(itemName)
                .ChangeCount(itemName, 10)
                .UpdateWishlist()
                .CheckPrice(itemName);
        }
        [AutomatedTest(4)]
        [Test, Description("Check move item from Wishlist -> Cart"), Order(2)]
        public void CheckMoving()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            wishlist.AddToCart_CheckBox_On(itemName)
                .AddToCart();
            cart.CheckCartItem(itemName);
            cart.RemoveItem(itemName);
            cart.UpdateCart();
        }
        [AutomatedTest(5)]
        [Test, Description("Check delete item from Wishlist"), Order(3)]
        public void CheckDelete()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.RemoveAllItems();
            wishlist.UpdateWishlist()
                .CheckEmptyWishlist();
        }
    }
}
