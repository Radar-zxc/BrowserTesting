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
    class CheckBooksSort : TestBase
    {
        private PageExplorer explorer;
        private BooksPage books;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            books = new BooksPage(Driver);
        }
        [Test]
        public void CheckSort()
        {
            explorer.OpenPage("books");
            books.CheckSort();
            explorer.OpenStartPage();
        }
    }
}
