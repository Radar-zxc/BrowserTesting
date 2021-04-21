using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
namespace BrowserTesting
{
    [TestFixture]
    public class SearchInYandex:TestBase
    {
        private readonly By Input_Search = By.Id("text");

        [OneTimeSetUp]
        public void driver_set_up()
        {
            driver.Navigate().GoToUrl("https://yandex.ru/");
        }
        [Test,Description("Search_with_enter"),Order(0)]
        public void Test1()
        {
            var enter = driver.FindElement(Input_Search);
            enter.Click();
            enter.SendKeys("ABOBA");
            enter.SendKeys(Keys.Enter);
            var check = driver.FindElement(By.XPath("//ul[@id='search-result']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        [Test , Description("Search_with_button"),Order(1)]
        public void Test2()
        {
            var enter = driver.FindElement(By.XPath("//input[@name='text']"));
            enter.Clear();
            string search_term = "Audi";
            enter.SendKeys(search_term);
            enter = driver.FindElement(By.ClassName("websearch-button__text"));
            enter.Click();
            Check_Search check_search = new Check_Search();
            check_search.Check(search_term, driver);
        }
    }
}
