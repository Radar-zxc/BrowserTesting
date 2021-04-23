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
namespace BrowserTesting
{
    public class TestBase
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void OpenBrowserWithJson()
        {
            var jsonText = File.ReadAllText("Appsettings.json");
            var convertedBrowser = JsonConvert.DeserializeObject<Browser>(jsonText);
            Browsers browser = (Browsers)Enum.Parse(typeof(Browsers),convertedBrowser.name);
            switch (browser){
                case Browsers.Firefox:
                    driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
                case Browsers.IE:
                    driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                    break;
                case Browsers.Edge:
                    driver = new OpenQA.Selenium.Edge.EdgeDriver();
                    break;
                default:
                    throw new Exception("Неудалось определить тип браузера");
            }
            driver.Manage().Window.Maximize();
        }
        [OneTimeSetUp]
        virtual public void DriverSetUp()
        {
            driver.Navigate().GoToUrl("https://www.google.ru/");
        }
        public void UrlVerify(string necessaryUrl)
        {
            string pageUrl = driver.Url;
            Assert.IsTrue(pageUrl.Contains(necessaryUrl),
                "Неверный Url после перехода на вкладку");
        }
        public void ContentVerify(string key)
        {
            string xpathCheck = "//div[@class='page-title']//h1[text()='" + key + "']";
            var check = driver.FindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        public void OpenPage(string pageName )
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + pageName + "']";
            var find = driver.FindElement(By.XPath(path));
            find.Click();
        }
        public void OpenPageWithList(string pageName,string pageElement)
        {
            string path = "//ul[@class='top-menu']//a[@href='/" + pageName + "']";
            var find = driver.FindElement(By.XPath(path));
            Actions actions = new Actions(driver);
            actions.MoveToElement(find).Build().Perform();
            path = "//ul[@class='top-menu']//ul[@class='sublist firstLevel active']//a[@href='/" + pageElement + "']";
            find = driver.FindElement(By.XPath(path));
            find.Click();
        }
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
