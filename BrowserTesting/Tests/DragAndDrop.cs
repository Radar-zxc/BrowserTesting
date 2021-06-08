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
    class DragAndDrop : TestBase
    {
        private DraggableElements elements;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_draggable");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            elements = new DraggableElements(Driver);
        }
        [Test]
        public void CheckDragAndDrop()
        {
            elements.StartCheck();
            elements.StartDragDrop_Primitive();
            elements.ClickWithJavaScript();
        }
    }
}
