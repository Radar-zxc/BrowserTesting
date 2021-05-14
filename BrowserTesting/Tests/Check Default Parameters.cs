using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting.Tests
{

    class Check_Default_Parameters:TestBase
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
        public void CheckDefaultParameters()
        {
            OpenPageWithList("computers", "desktops");
            explorer.GoToItemPage("Build your own computer");
            computer.StartCheckDefaultParameters();
        }
    }
}
