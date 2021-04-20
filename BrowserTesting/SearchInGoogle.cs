using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
namespace BrowserTesting
{
    public class SearchInGoogle:Open_Close_Browser
    {
        private readonly By Input_Search = By.XPath("//input[@class='gLFyf gsfi']");

        [Test]
        public void Test1()
        {
            Open_browser(ref driver, "https://www.google.ru/");
            var enter = driver.FindElement(Input_Search);
            enter.Click();
            enter.SendKeys("ABOBA");
            enter.SendKeys(Keys.Enter);
            var check = driver.FindElement(By.XPath("//div[@id='rso']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");

            enter = driver.FindElement(Input_Search);
            string search_term = "Audi";
            enter.Clear();
            enter.SendKeys(search_term);
            enter.SendKeys(Keys.Enter);
            Check_Search check_search = new Check_Search();
            check_search.Check(search_term, driver);
            Close_browser(driver);
        }
    }
}