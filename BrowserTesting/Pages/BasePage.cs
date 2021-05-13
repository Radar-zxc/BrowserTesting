using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Enums;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace BrowserTesting
{
    abstract class BasePage
    {
        protected IWebDriver Driver ;
        public BasePage(IWebDriver Driver )
        {
            this.Driver = Driver;
        }
        public void RemoveCart(By removeButton)
        {
            var action = Driver.FindElement(removeButton);
            action.Click();
        }
        public void ChangeCount(By countField ,int newCount)
        {
            var action = Driver.FindElement(countField);
            action.Click();
            action.Clear();
            action.SendKeys(newCount.ToString());
            action.SendKeys(Keys.Enter);
        }
        public void AddItem(By addButton)
        {
            var action = Driver.FindElement(addButton);
            action.Click();
        }
        public void RefreshCart(By updateButton)
        {
            var action = Driver.FindElement(updateButton);
            action.Click();
        }
    }
}
