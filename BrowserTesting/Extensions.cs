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
using OpenQA.Selenium.Support.Extensions;

namespace BrowserTesting
{
    static class Extensions
    {
        /// <summary>
        /// Метод, сокращающий цепочку методов для выполения передвижения к элементу 
        /// </summary>
        public static IWebElement MoveToElement (this IWebDriver Driver,IWebElement elem )
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(elem).Build().Perform();
            return elem;
        }
    }
}
