using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class CheckBooksSort : TestBase
    {
        private PageExplorer explorer;
        private PreOrderPage page;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            page = new PreOrderPage(Driver);
        }
        [Test, Description("Check Sorts on books page"), Order(0)]
        public void CheckSort()
        {
            explorer.OpenPage("books");
            page.CheckSort();
            explorer.OpenStartPage();
        }
    }
}
