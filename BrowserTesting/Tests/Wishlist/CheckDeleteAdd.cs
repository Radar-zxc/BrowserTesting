using NUnit.Framework;
using BrowserTesting.Pages;
using System;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace BrowserTesting.Wishlist
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
        }/*
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = (typeof(CheckDeleteAdd).Name).ToString();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\" + $"{className} {DateTime.Now.Date.ToShortDateString()}.html";
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
                string className = (typeof(CheckDeleteAdd).Name).ToString();
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
        [AutomatedTest(7)]
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
