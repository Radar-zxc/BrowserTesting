using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
namespace BrowserTesting
{
    public class SearchInGoogle
    {
        private IWebDriver driver;
        private readonly By Input_Search = By.XPath("//input[@class='gLFyf gsfi']");

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Navigate().GoToUrl("https://www.google.ru/");
            driver.Manage().Window.Maximize();

        }

        [Test]
        public void Test1()
        {
            var enter = driver.FindElement(Input_Search);
            enter.Click();
            enter.SendKeys("ABOBA");
            enter.SendKeys(Keys.Enter);
            var check = driver.FindElement(By.XPath("//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");

            enter = driver.FindElement(Input_Search);
            string search_term = "Audi";
            enter.Clear();
            enter.SendKeys(search_term);
            enter.SendKeys(Keys.Enter);
            Check_Search check_search = new Check_Search();
            check_search.Check(search_term, driver);
             
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}