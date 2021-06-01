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
using System.Collections;
using System.Linq;

namespace BrowserTesting.Pages
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для осуществления
    /// переходов между страницами и вкладками
    /// </summary>
    class PageExplorer : BasePage
    {
        private IEnumerable<string> tabsList;
        private List<string> tabsDescriptorList = new List<string>();
        public PageExplorer(IWebDriver Driver):base(Driver)
        {
        }
        /// <summary>
        /// Метод открытия страницы с указаным названием из заголовка сайта
        /// </summary>
        public void OpenPage(string pageName)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{pageName}']";
            ClickOnElement(By.XPath(path));
        }
        /// <summary>
        /// Метод открытия страницы из заголовка сайта , находящейся в выпадающем списке 
        /// </summary>
        public void OpenPageWithList(string pageName, string pageElement)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{ pageName}']";
            var find = Driver.FindElement(By.XPath(path));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(find).Build().Perform();
            path = $"//ul[@class='top-menu']//ul[@class='sublist firstLevel active']//a[@href='/{ pageElement}']";
            ClickOnElement(By.XPath(path));
        }
        /// <summary>
        /// Метод открытия стартовой страницы сайта Tricentis Demo Web Shop путем нажатия на логотип
        /// </summary>
        public void OpenStartPage()
        {
            string path = "//a[normalize-space(text()='Tricentis Demo Web Shop')]//img[@title='']";
            ClickOnElement(By.XPath(path));
        }
        /// <summary>
        /// Метод открытия страницы корзины
        /// </summary>
        public void OpenCart()
        {
            By cart = By.XPath("//div[@class='header-links-wrapper']//a[@class='ico-cart']//span[@class='cart-label']");
            ClickOnElement(cart);
        }
        /// <summary>
        /// Метод открытия страницы товара из списка по заданому имени 
        /// </summary>
        public void GoToItemPage(string itemName)
        {
            string pathItem = $"//div[@class='page-body']//div[@class='item-box']//a[text()='{ itemName}']";
            ClickOnElement(By.XPath(pathItem));
        }
        /// <summary>
        /// Метод проверки перехода на страницу корзины 
        /// </summary>
        public void CheckCartTravel(string url)
        {
            Assert.AreEqual(Driver.Url , url, "Осуществлен неверный переход при попытке перейти в корзину");
        }
        /// <summary>
        /// Метод открытия страницы в новой вкладке по заданному имени
        /// </summary>
        public void OpenPageInNewTab(string pageName)
        {
            string path = $"//ul[@class='top-menu']//a[@href='/{pageName}']";
            By newPage = By.XPath(path);
            if (tabsList == null)
            {
                AddTabInDescriptorList();
            }
            OpenPageRef(newPage);
            RefreshTabsList();
            Driver.SwitchTo().Window(tabsList.Last());
            OpenPage(pageName);
            AddTabInDescriptorList();
        }
        /// <summary>
        /// Метод добавляющий новую открытую вкладку в список
        /// </summary>
        private void AddTabInDescriptorList()
        {
            tabsDescriptorList.Add(Driver.CurrentWindowHandle);
        }
        /// <summary>
        /// Метод обновляющий весь список открытых вкладок
        /// </summary>
        private void RefreshTabsList()
        {
            tabsList = Driver.WindowHandles;
        }
        /// <summary>
        /// Метод перехода на вкладку с заданной очередностью ее открытия
        /// </summary>
        public void GoToTab(int tabNubmer)
        {
            RefreshTabsList();
            Assert.IsTrue(tabNubmer < tabsDescriptorList.Count && tabNubmer >= 0,
                "Попытка обращения к вкладке, которая не могла существовать");
            bool found=false;
            foreach (string tab in tabsList)
            {
                if (tab == tabsDescriptorList[tabNubmer])
                {
                    Driver.SwitchTo().Window(tab);
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found,"Попытка обращения к удаленнной вкладке");
        }
        /// <summary>
        /// Метод, сравнивающий текущий URL с передаваемым в качестве параметра, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void UrlVerify(string necessaryUrl)
        {
            string pageUrl = Driver.Url;
            Assert.IsTrue(pageUrl.Contains(necessaryUrl),
                "Неверный Url после перехода на вкладку");
        }
        /// <summary>
        /// Метод проверки открытия вкладки с заданным именем
        /// </summary>
        public void ContentVerify(string key)
        {
            string xpathCheck = "//div[@class='page-title']//h1[text()='" + key + "']";
            var check = Driver.FindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
    }
}
