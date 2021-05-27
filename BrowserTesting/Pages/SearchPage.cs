using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace BrowserTesting.Pages
{
    class SearchPage : BasePage
    {
        public SearchPage(IWebDriver Driver) : base(Driver)
        {
        }
        private readonly By SearchKeywordField = By.XPath("//input[@class='search-text valid']");
        private readonly By AdvancedSearchCheckBox = By.XPath("//input[@id='As']");
        private readonly By CategoryPopUpList = By.XPath("//select[@id='Cid']//option");
        private readonly By SupCategoriesCheckBox = By.XPath("//input[@id='Isc']");
        private readonly By ManufacturerPopUpList = By.XPath("//select[@id='Mid']");
        private readonly By PriceField_From = By.XPath("//input[@id='Pf']");
        private readonly By PriceField_To = By.XPath("//input[@id='Pt']");
        private readonly By SearchInDecsriptionCheckBox = By.XPath("//input[@id='Sid']");
        private readonly By SearchButton = By.XPath("//input[@class='button-1 search-button']");
        private void UpdateSearch()
        {
            Driver.FindElement(SearchButton).Click();
        }
        public void NewRequest(string request)
        {
            Driver.FindElement(SearchKeywordField).Clear();
            Driver.FindElement(SearchKeywordField).SendKeys(request);
        }
        public void CheckBox_TurnOn(By checkBox)
        {
            IWebElement elem = Driver.FindElement(checkBox);
            if (!elem.Selected)
            {
                elem.Click();
            }
        }
        public void CheckBox_TurnOff(By checkBox)
        {
            IWebElement elem = Driver.FindElement(checkBox);
            if (elem.Selected)
            {
                elem.Click();
            }
        }
       public List<string> ListCategories()
        {
            List<string> list = new List<string>();
            int i = 0;
            list = (from elem in Driver.FindElements(By.XPath("sda"))
                   where  i<4
                   select elem , i++).ToList();
            var asd = u=>Driver.FindElements(By.XPath("sda")).Where(u => i < 4).Select(u);
        }
    }
}
