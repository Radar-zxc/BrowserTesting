﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting.Tests
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
