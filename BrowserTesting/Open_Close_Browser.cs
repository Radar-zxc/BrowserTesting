using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace BrowserTesting
{
    public class Open_Close_Browser
    {
        public IWebDriver driver;
        public void Open_browser (ref IWebDriver driver ,string url)
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }
        public void Close_browser(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}
