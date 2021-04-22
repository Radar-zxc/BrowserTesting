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
            FileStream fs = new FileStream("Appsettings.json", FileMode.Open);
            byte[] array = new byte[fs.Length];
            fs.Read(array);
            string info = System.Text.Encoding.Default.GetString(array);
            Browser restoredBrowser = JsonSerializer.Deserialize<Browser>(info);
            string browser = restoredBrowser.name;
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
                default:
                    throw new Exception("Некорректное содержание Json файла");
            }
            driver.Manage().Window.Maximize();
            fs.Close();
        }
        [OneTimeTearDown]
        public void Close_browser()
        {
            driver.Quit();
        }
    }
}
