using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace BrowserTesting
{
    class AddToCart:TestBase
    {
        override public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [Test,Description("Add jewelry to cart"),Order(0)]
        public void AddJewelry()
        {
            OpenPage("jewelry");
            CheckItemNames("Black & White Diamond Heart");
            AddMoreItems(50);
            CheckManyItemPrice(50);
        }
    }
}
