using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
namespace BrowserTesting
{
    public class TestBase
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void Open_browser()
        {
            var JsonText = File.ReadAllText("Appsettings.json");
            var browser = JsonConvert.DeserializeObject<Browser>(JsonText);
            switch (browser.name){
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
                default:
                    throw new Exception("Некорректное содержание Json файла");
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
