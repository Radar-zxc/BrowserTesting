using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
namespace BrowserTesting
{
    public class Check_Search
    {
        public static void Check(string keys , IWebDriver driver)
        {
            string xpath_check = ".//*[text()='" + keys + "']";
            var check = driver.FindElement(By.XPath(xpath_check));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
    }
}
