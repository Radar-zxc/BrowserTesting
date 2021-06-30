using NUnit.Framework;
using BrowserTesting.Pages;
using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace BrowserTesting.Tests.Wishlist
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckDeleteAdd_InDiffTabs : TestBase
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
        public CheckDeleteAdd_InDiffTabs(string item1)
        {
            itemName = item1;
        }
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = (typeof(CheckDeleteAdd_InDiffTabs).Name).ToString();
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
                string className = (typeof(CheckDeleteAdd_InDiffTabs).Name).ToString();
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
        }
        [AutomatedTest(8)]
        [Test, Description("Removing one item from the Wishlist and adding it to Cart in different browser tabs"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            explorer.OpenPageInNewTab("wishlist");
            wishlist.RemoveItem(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
            explorer.GoToTab(0);
            wishlist.AddToCart_CheckBox_On(itemName)
                .UpdateWishlist();
            explorer.CheckCartTravel();
            cart.CheckCartItem(itemName);
            cart.RemoveAllItems();
            explorer.CloseWindow(1);
        }
    }
}
