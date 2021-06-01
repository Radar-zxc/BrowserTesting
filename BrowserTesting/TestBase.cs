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
    /// <summary>
    /// Это класс, содержащий в себе базовые методы, необходимые для выполнения тестов
    /// </summary>
    public class TestBase
    {
        protected IWebDriver Driver;
        /// <summary>
        /// Метод получения информации из Json файла о том, какой драйвер будет использован для выполнения тестов
        /// </summary>
        [OneTimeSetUp]
        public void OpenBrowserWithJson()
        {
            var jsonText = File.ReadAllText("Appsettings.json");
            var convertedBrowser = JsonConvert.DeserializeObject<Browser>(jsonText);
            Browsers browser = (Browsers)Enum.Parse(typeof(Browsers),convertedBrowser.name);
            switch (browser){
                case Browsers.Firefox:
                    Driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    Driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
                case Browsers.IE:
                    Driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                    break;
                case Browsers.Edge:
                    Driver = new OpenQA.Selenium.Edge.EdgeDriver();
                    break;
                default:
                    throw new Exception("Неудалось определить тип браузера");
            }
            Driver.Manage().Window.Maximize();
        }
        /// <summary>
        /// Метод открытия стартового сайта
        /// </summary>
        [OneTimeSetUp]
        virtual public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("https://www.google.ru/");
        }
        /// <summary>
        /// Метод, изменяющий региональные особенности на en-US
        /// </summary>
        [OneTimeSetUp]
        public void ChangeCultureToUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        /// <summary>
        /// Метод, изменяющий региональные особенности на ru-RU
        /// </summary>
        [OneTimeTearDown]
        public void ChangeCultureToRU()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
        }
        /// <summary>
        /// Метод завершения работы драйвера
        /// </summary>
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
