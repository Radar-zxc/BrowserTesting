using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace BrowserTesting
{
    public static class Asserts
    {
        /// <summary>
        /// Метод, сравнивающий текущий URL с передаваемым в качестве параметра, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public static void UrlVerify(IWebDriver Driver, string necessaryUrl)
        {
            string pageUrl = Driver.Url;
            Assert.IsTrue(pageUrl.Contains(necessaryUrl),
                "Неверный Url после перехода на страницу");
        }
        /// <summary>
        /// Метод проверки открытия вкладки с заданным именем
        /// </summary>
        public static void ContentVerify(IWebDriver Driver ,string key)
        {
            string xpathCheck = "//div[@class='page-title']//h1[text()='" + key + "']";
            var check = Driver.FindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
    }
}
