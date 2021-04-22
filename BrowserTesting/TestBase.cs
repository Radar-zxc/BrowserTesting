using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using System.Text.Json;
namespace BrowserTesting
{
    public class TestBase
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void Open_browser()
        {
            string browser =  Browsers.JsonRead.Read_file();
            switch(browser){
                case "Firefox":
                    driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                    break;
                case "Chrome":
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
                case "IE":
                    driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                    break;
                case "Edge":
                    driver = new OpenQA.Selenium.Edge.EdgeDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
        }
        [OneTimeTearDown]
        public void Close_browser()
        {
            driver.Quit();
        }
    }
}
