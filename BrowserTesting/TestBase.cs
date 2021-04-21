using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace BrowserTesting
{
    public class TestBase
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void Open_browser()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Manage().Window.Maximize();
        }
        [OneTimeTearDown]
        public void Close_browser()
        {
            driver.Quit();
        }
    }
}
