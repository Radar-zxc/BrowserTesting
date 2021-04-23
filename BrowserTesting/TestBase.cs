using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
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
        [OneTimeSetUp]
        virtual public void DriverSetUp()
        {
            driver.Navigate().GoToUrl("https://www.google.ru/");
        }
        public void UrlVerify(string NecessaryUrl)
        {
            string PageUrl = driver.Url;
            Assert.IsTrue(PageUrl.Contains(NecessaryUrl),
                "Неверный Url после перехода на вкладку");
        }
        public void ContentVerify(string key)
        {
            string xpath_check = "//div[@class='page-title']//h1[text()='" + key + "']";
            var check = driver.FindElement(By.XPath(xpath_check));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        public void OpenPage(string PageName )
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + PageName + "']";
            var find = driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenPageWithList(string PageName,string PageElement)
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + PageName + "']";
            var find = driver.FindElement(By.XPath(path));
            Actions actions = new Actions(driver);
            actions.MoveToElement(find).Build().Perform();
            path = "//ul[@class='top-menu']//a[@href='/" + PageElement + "']";
            find = driver.FindElement(By.XPath(path));
            find.Click();
        }
        [OneTimeTearDown]
        public void Close_browser()
        {
            driver.Quit();
        }
    }
}
