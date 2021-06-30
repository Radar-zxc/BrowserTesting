using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using Newtonsoft.Json;
using BrowserTesting.Enums;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Configuration;
using OpenQA.Selenium.Interactions;
using NUnit.Framework.Interfaces;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий в себе базовые методы, необходимые для выполнения тестов
    /// </summary>
    [TestFixtureSource("asd")]
    public class TestBase
    {
        protected IWebDriver Driver;
        public static ExtentV3HtmlReporter htmlReporter;
        public static ExtentReports extent = new ExtentReports();
        public static ExtentTest test;
        StreamWriter file;
        /// <summary>
        /// Метод получения информации из Json файла о том, какой драйвер будет использован для выполнения тестов
        /// </summary>
        [OneTimeSetUp]
        public void OpenBrowserWithJson()
        {
            var jsonText = File.ReadAllText("Appsettings.json");
            var convertedBrowser = JsonConvert.DeserializeObject<Browser>(jsonText);
            Browsers browser = (Browsers)Enum.Parse(typeof(Browsers), convertedBrowser.name);
            switch (browser)
            {
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
            if (WindowOptions.WindowAutoMaxSize)
            {
                Driver.Manage().Window.Maximize();
            }
            else
            {
                Driver.Manage().Window.Size = new System.Drawing.Size(WindowOptions.Window_x, WindowOptions.Window_y);
            }
        }
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = "BrowserTesting";//(typeof(BrowserTesting).Name).ToString();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\" + $"{className} {DateTime.Now.Date.ToShortDateString()}.html";
            htmlReporter = new ExtentV3HtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }
        [TearDown]
        public void GetScreenshotWhenFail()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                string className = "BrowserTesting";//(typeof(WishlistTesting).Name).ToString();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string screenshotName = $"{className} {DateTime.Now.Date.ToShortDateString()}.png";
                string screenshotPath = projectPath + "Reports\\" + screenshotName;
                Screenshot file = ((ITakesScreenshot)Driver).GetScreenshot();
                //file.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                test.Log(Status.Fail, "Test ended with " + Status.Fail + '\r' + '\n' + TestContext.CurrentContext.Result.StackTrace);
                //test.Fail("Fail screenshot: ",
                //MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotName).Build());

                Screenshot screenshot = (Driver as ITakesScreenshot).GetScreenshot();
                string a = screenshot.AsBase64EncodedString;
                test.Log(Status.Fail, "Fail screenshot",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(a).Build());  
            }
        }
        /// <summary>
        /// Метод логирования результатов после завершения теста
        /// </summary>
        [TearDown]
        virtual public void LogTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Status logStatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logStatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logStatus = Status.Skip;
                    break;
                default:
                    logStatus = Status.Pass;
                    break;
            }
            if (logStatus != Status.Pass)
            {
                test.Log(logStatus, TestContext.CurrentContext.Result.Message);
            }
            test.Info("Start time: " + test.Extent.ReportStartDateTime.ToString());
            test.Info("End time: " + test.Extent.ReportEndDateTime.ToString());
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
        /// Метод завершения работы драйвера
        /// </summary>
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
