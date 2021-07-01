using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using Newtonsoft.Json;
using BrowserTesting.Enums;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using Selenium = OpenQA.Selenium;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий в себе базовые методы, необходимые для выполнения тестов
    /// </summary>
    public class TestBase
    {
        protected IWebDriver Driver;
        public static ExtentV3HtmlReporter htmlReporter;
        public static ExtentReports extent = new ExtentReports();
        public static ExtentTest test;
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
                    Driver = new Selenium.Firefox.FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    Driver = new Selenium.Chrome.ChromeDriver();
                    break;
                case Browsers.IE:
                    Driver = new Selenium.IE.InternetExplorerDriver();
                    break;
                case Browsers.Edge:
                    Driver = new Selenium.Edge.EdgeDriver();
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
        /// <summary>
        /// Метод инициализации доклада 
        /// </summary>
        [OneTimeSetUp]
        public void InitReport()
        {
            string className = "BrowserTesting";
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\" + $"{className} {DateTime.Now.Date.ToShortDateString()}.html";
            htmlReporter = new ExtentV3HtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
            htmlReporter.Start();
        }
        /// <summary>
        /// Метод логирования результатов после завершения теста
        /// </summary>
        [TearDown]
        public void LogTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Status logStatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    Screenshot screenshot = (Driver as ITakesScreenshot).GetScreenshot();
                    string base64 = screenshot.AsBase64EncodedString;
                    test.Log(Status.Info, "Fail screenshot",
                        MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64).Build());
                    test.Log(Status.Info, "Test ended with " + Status.Fail + '\r' + '\n' + TestContext.CurrentContext.Result.StackTrace);
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
        /// Метод завершения работы драйвера и записи информации о тесте в Report
        /// </summary>
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            extent.Flush();
            Driver.Quit();
        }
    }
}
