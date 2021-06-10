using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace BrowserTesting
{
    class CheckFiltersByPrice : TestBase
    {
        private PreOrderPage page;
        private PageExplorer explorer;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            page = new PreOrderPage(Driver);
            explorer = new PageExplorer(Driver);
        }
        [Test, Description("Сhecking the sorting by price on the Jewelry Page")]
        public void CheckFiltersInJewelryPage()
        {
            explorer.OpenPage("jewelry");
            page.ChooseFilter("0-500")
                .CheckTable()
                .ChooseFilter("700-3000")
                .CheckTable()
                .ChooseFilter("500-700")
                .CheckTable();
            explorer.OpenStartPage();
        }
    }
}
