﻿using System;
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

namespace BrowserTesting
{
    /// <summary>
    /// /Класс, содержащий общие методы для других классов страниц
    /// </summary>
    abstract class BasePage 
    {
        protected IWebDriver Driver ;
        public BasePage(IWebDriver Driver )
        {
            this.Driver = Driver;
        }
        /// <summary>
        /// Метод изменения количества предметов по заданному локатору поля на заданное новое количество
        /// </summary>
        protected void ChangeCount(By countField, int newCount)
        {
            var action = Driver.FindElement(countField);
            action.Clear();
            action.SendKeys(newCount.ToString());
        }
        /// <summary>
        /// Метод нажатия на элемент по заданному локатору
        /// </summary>
        protected void ClickOnElement(By elem)
        {
            var action = Driver.FindElement(elem);
            action.Click();
            //для IE Driver.MoveToElement(action).Click();
        }
        /// <summary>
        /// Метод выбора определенного параметра в выпадающем списке по соответствующим локаторам 
        /// </summary>
        protected void PickParameterInPopupList(By popupList, By popupParameter)
        {
            var list = Driver.FindElement(popupList);
            Driver.MoveToElement(list).Click();
            ClickOnElement(popupParameter);
        }
        /// <summary>
        /// Метод открытия страницы с заданным локатором в новой вкладке
        /// </summary>
        protected void OpenPageRef(By page)
        {
            Driver.FindElement(page).SendKeys(Keys.Control + Keys.Shift + Keys.Enter);
        }
    }
}
