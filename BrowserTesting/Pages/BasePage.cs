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
        //public IWebElement Element;
        public BasePage(IWebDriver Driver )
        {
            this.Driver = Driver;
        }
        /*public void RemoveCart(By itemRemoveButton)
        {
            var remove = Driver.FindElement(itemRemoveButton);
            remove.Click();
            remove.SendKeys(Keys.Enter);
        }*/
        /*public void ChangeCountInCart(string itemName,int count)
        {
            string pathCountItem = $"//a[text()='{itemName}']/../..//input[@class='qty-input']";
            var change = Driver.FindElement(By.XPath(pathCountItem));
            change.Click();
            change.SendKeys(count.ToString());
            change.SendKeys(Keys.Enter);
        }*/

        /*public void ChangeCountInItemPage(By itemCountField ,int count)
        {
            //string pathCountItem = "//input[@class='qty-input']";
            var change = Driver.FindElement(itemCountField);
            change.Click();
            change.SendKeys(count.ToString());
            change.SendKeys(Keys.Enter);
        }*/

        /*public void Click()
        {
            Element.Click();
        }*/
        public void RemoveCart(IWebElement removeButton)
        {
            removeButton.Click();
        }
        public void ChangeCount(IWebElement countField ,int newCount)
        {
            countField.Click();
            countField.Clear();
            countField.SendKeys(newCount.ToString());
            countField.SendKeys(Keys.Enter);
        }
        public void AddItem(IWebElement addButton)
        {
            addButton.Click();
        }
    }
}
