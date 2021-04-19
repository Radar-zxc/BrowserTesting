﻿using System;
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
            enter = driver.FindElement(By.CssSelector("div [class=serp-adv__found]"));
            Assert.IsTrue(enter.Displayed);
            Thread.Sleep(2000);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
