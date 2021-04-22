using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.IO;
using System.Text.Json;
namespace BrowserTesting
{
    [TestFixture]
    public class SearchInGoogle : TestBase
    {
        private readonly By Input_Search = By.XPath("//input[@name='q']");

        [OneTimeSetUp]
        public void driver_set_up()
        {
            driver.Navigate().GoToUrl("https://www.google.ru/");
        }
        [Test, Description("Search_with_enter"), Order(0)]
        public void Search_with_enter()
        {
            var enter = driver.FindElement(Input_Search);
            enter.Click();
            enter.SendKeys("ABOBA");
            enter.SendKeys(Keys.Enter);
            var check = driver.FindElement(By.XPath("//div[@id='rso']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        [Test, Description("Search_with_button"), Order(1)]
        public void Search_with_button()
        {
            var enter = driver.FindElement(Input_Search);
            enter.Clear();
            string search_term = "Audi";
            enter.SendKeys(search_term);
            enter = driver.FindElement(By.XPath("//button"));
            enter.Click();
            Check_Search.Check(search_term, driver);
        }
    }
}
