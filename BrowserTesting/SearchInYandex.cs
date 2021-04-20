using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace BrowserTesting
{
    class SearchInYandex
    {
        private IWebDriver driver;
        private readonly By Input_Search = By.Id("text");

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            
            driver.Navigate().GoToUrl("https://yandex.ru/");
            driver.Manage().Window.Maximize();

        }

        [Test]
        public void Test1()
        {
            var enter = driver.FindElement(Input_Search);
            enter.Click();
            enter.SendKeys("ABOBA");
            enter.SendKeys(Keys.Enter);
            var check = driver.FindElement(By.XPath("//ul[@id='search-result']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");

            enter = driver.FindElement(By.XPath("//input[@name='text']"));
            string search_term = "Audi";
            enter.Clear();
            enter.SendKeys(search_term);
            enter = driver.FindElement(By.ClassName("websearch-button__text"));
            enter.Click();
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
