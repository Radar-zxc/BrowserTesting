using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting.Tests
{

    class CheckDefaultParameters : TestBase
    {
        private PageExplorer explorer;
        private ComputerPage computer;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            computer = new ComputerPage(Driver);
        }
        [Test, Description("Check default parameters on 'Build your own computer' page"), Order(0)]
        public void CheckParameters()
        {
            explorer.OpenPageWithList("computers", "desktops");
            explorer.GoToItemPage("Build your own computer");
            computer.StartCheckDefaultParameters();
            explorer.OpenStartPage();
        }
    }
}
