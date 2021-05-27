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

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий общие методы для других классов страниц
    /// </summary>
    abstract class BasePage
    {
        protected IWebDriver Driver ;
        public BasePage(IWebDriver Driver )
        {
            this.Driver = Driver;
        }
        /// <summary>
        /// Метод нажатия на кнопку удаления предмета из корзины по локатору кнопки
        /// </summary>
        protected void RemoveCart(By removeButton)
        {
            var action = Driver.FindElement(removeButton);
            action.Click();
        }
        /// <summary>
        /// Метод изменения количества предметов по заданному локатору поля на заданное новое количество
        /// </summary>
        protected void ChangeCount(By countField ,int newCount)
        {
            var action = Driver.FindElement(countField);
            action.Click();
            action.Clear();
            action.SendKeys(newCount.ToString());
            action.SendKeys(Keys.Enter);
        }
        /// <summary>
        /// Метод добавления предмета в корзину по заданному локатору кнопки добавления 
        /// и последующего нажатия на нее
        /// </summary>
        protected void AddItem(By addButton)
        {
            var action = Driver.FindElement(addButton);
            action.Click();
        }
        /// <summary>
        /// Метод нажатия кнопки Update shoping cart по заданному локатору
        /// </summary>
        protected void RefreshCart(By updateButton)
        {
            var action = Driver.FindElement(updateButton);
            action.Click();
        }
        /// <summary>
        /// Метод выбора определенного параметра в выпадающем списке по соответствующим локаторам
        /// </summary>
        protected void PickParameterInPopupList(By popupList, By popupParameter)
        {
            Actions move = new Actions(Driver);
            var list = Driver.FindElement(popupList);
            move.MoveToElement(list).Build().Perform();
            list = Driver.FindElement(popupParameter);
            list.Click();
        }
        /// <summary>
        /// Метод открытия страницы с заданным локатором в новой вкладке
        /// </summary>
        protected void OpenPageRef(By page)
        {
            var elem = Driver.FindElement(page);
            Actions newTab = new Actions(Driver);
            newTab
                .KeyDown(Keys.Control)
                .KeyDown(Keys.Shift)
                .Click(elem).KeyUp(Keys.Control).KeyUp(Keys.Shift)
                .Build()
                .Perform();
        }
    }
}
