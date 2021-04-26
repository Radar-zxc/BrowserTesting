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
        [Test, Description("Add jewelry to cart"), Order(0)]
        public void AddJewelry_MultiplyInCart()
        {
            OpenPage("jewelry");
            CheckItemNames("Black & White Diamond Heart");
            AddMoreItems(50);
            CheckManyItemPrice(50);
           
        }
        [Test , Order(1)]
        public void AddJewelry_MultiplyInItemPage()
        {
            RemoveCart("Black & White Diamond Heart", 50);
            OpenPage("jewelry");
            string s = GoToJewelryItemPage("Black & White Diamond Heart");
            CheckItemNamesInItemPage(s);
            AddItemFromPage();
        }
    }
}
