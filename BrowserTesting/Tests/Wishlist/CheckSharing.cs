using NUnit.Framework;
using BrowserTesting.Pages;
using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace BrowserTesting
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckSharing :TestBase
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
        public CheckSharing(string item1)
        {
            itemName = item1;
        }
        /*
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = (typeof(CheckSharing).Name).ToString();
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
                string className = (typeof(CheckSharing).Name).ToString();
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
        [AutomatedTest(9)]
        [Test, Description("Check Wishlist URL for sharing function"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.CheckItem(itemName);
            explorer.OpenPageInNewTab("share-link");
            wishlist.CheckItem(itemName);
            explorer.CloseCurrentWindow();
        }
    }
}
