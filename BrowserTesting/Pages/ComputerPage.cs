using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
namespace BrowserTesting
{
    class ComputerPage : OrderPage
    {
        readonly string defaultProcessor = "2.5 GHz Intel Pentium Dual-Core E2200 [+15.00]";
        readonly string defaultRAM = "2 GB";
        readonly string defaultHDD = "";
        readonly string defaultOS = "Ubuntu";
        readonly string[] defaultSoftware = new string[] {"Microsoft Office [+50.00]" };
        readonly string defaultCount = "1";
        readonly string defaultPriceColor = Colors.Red;
        public ComputerPage(IWebDriver Driver) : base(Driver)
        {

        }
        public IWebElement ProcessorList()
        {
            string path = "//div[@class='attributes']//select[@id='product_attribute_16_5_4']";
            IWebElement list = Driver.FindElement(By.XPath(path));
            return list;
        }
        public IWebElement RAM_List()
        {
            string path = "//div[@class='attributes']//select[@id='product_attribute_16_6_5']";
            IWebElement list = Driver.FindElement(By.XPath(path));
            return list;
        }
        public void CheckDefaultProcessor()
        {
            SelectElement list = new SelectElement(ProcessorList());
            string nameProcessor = list.SelectedOption.Text;
            Assert.AreEqual(defaultProcessor, nameProcessor, "Фактический процессор не соответствует требуемому по умолчанию");
        }
        public void CheckDefault_RAM()
        {
            SelectElement list = new SelectElement(RAM_List());
            string nameRAM = list.SelectedOption.Text;
            Assert.AreEqual(defaultRAM, nameRAM, "Фактический RAM не соответствует требуемому по умолчанию");
        }
        public ReadOnlyCollection<IWebElement> HDD_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        public void CheckDefault_HDD()
        {
            string nameHDD = "";
            ReadOnlyCollection<IWebElement> listHDD = HDD_Buttons();
            int i = 0;
            bool check = true;
            while (i < listHDD.Count && check)
            {
                if (listHDD[i].Selected)
                {
                    nameHDD = listHDD[i].FindElement(
                        By.XPath($"(//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/label)[{i+1}]"))
                        .Text;
                    Assert.AreEqual(defaultHDD, nameHDD, "Фактический HDD не соответствует требуемому по умолчанию");
                    check = false;
                }
                i++;
            }
            /*foreach (IWebElement parameter in HDD_Buttons())
            {
                if (parameter.Selected)
                {
                    nameHDD = parameter.FindElement(
                        By.XPath("(//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/label)[{count}]"))
                        .Text;
                }
                count++;
            }*/
        }
        public ReadOnlyCollection<IWebElement> OS_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Проверка выбранной RadioButton для списка OS,
        /// возврат названия параметра соответсвтующей RadioButton
        /// </summary>
        public void CheckDefault_OS()
        {
            string nameOS = "";
            ReadOnlyCollection<IWebElement> listOS = OS_Buttons();
            int i = 0;
            bool check = true;
            while (i < listOS.Count && check)
            {
                if (listOS[i].Selected)
                {
                    nameOS = listOS[i].FindElement(
                        By.XPath($"(//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/label)[{i + 1}]"))
                        .Text;
                    Assert.AreEqual(defaultOS, nameOS, "Фактическая OS не соответствует требуемой по умолчанию");
                    check = false;
                }
                i++;
            }
            /*foreach (IWebElement parameter in OS_Buttons())
            {
                if (parameter.Selected)
                {
                    nameOS = parameter.FindElement(By.XPath
                        ($"(//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/label)[{count}]"))
                        .Text;
                }
                count++;
            }
            return nameOS;*/
        }
        /// <summary>
        /// Поиск и формирование списка доступных параметров для выбора у Software
        /// </summary>
        public ReadOnlyCollection<IWebElement> SoftwareList()
        {
            string path = "//dl//dt//label[normalize-space(text())='Software']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Проверка выбранных элементов в Checkbox при списке Software,
        /// возврат списка выбранных элементов
        /// </summary>
        public void CheckDefaultSoftware()
        {
            List<string> listSoftware = new List<string>();
            int count = 1;
            foreach(IWebElement parameter in SoftwareList())
            {
                if (parameter.Selected)
                {
                    listSoftware.Add(Driver.FindElement
                        (By.XPath($"(//dl//dt//label[normalize-space(text())='Software']//../following::dd[1]//li/label)[{count}]"))
                        .Text);
                }
            }
            for (int i = 0; i < listSoftware.Count; i++)
            {
                for (int j = 0; j < defaultSoftware.Length;j++)
                {
                    Assert.AreEqual(defaultSoftware[j], listSoftware[i], 
                        "Фактический параметр Software не соответствует требуемому по умолчанию");
                }
            }
        }
        /// <summary>
        /// Проверка значения количества по умолчанию в соответствующем поле,
        /// возврат этого значения ввиде строки
        /// </summary>
        public void CheckDefaultCount()
        {
            SetItemCountField();
            string itemCount = Driver.FindElement(itemCountField).GetAttribute("value");
            Assert.AreEqual(defaultCount, itemCount,
                "Фактическое значение параметра Qty не соответствует требуемому по умолчанию");
        }
        /// <summary>
        /// Проверка соответствия фактического цвета цены товара красному цвету, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckPriceColor_RED()
        {
            String buttonTextColor = Driver.FindElement(By.XPath("//span[@itemprop='price']")).GetCssValue("color");
            Assert.AreEqual(defaultPriceColor, buttonTextColor, "Цвет цены предмета не красный");
            //return buttonTextColor == Colors.Red;
        }
        ///<summary>
        ///Главная функция проверки парамаетров по умолчанию 
        ///для страницы "Build your own computer"
        ///</summary>
        public void StartCheckDefaultParameters()
        {
            List<string> defaultParameters = new List<string>();
            /*defaultParameters.Add(CheckDefaultProcessor());
            defaultParameters.Add(CheckDefault_RAM());
            defaultParameters.Add(CheckDefault_HDD());
            defaultParameters.Add(CheckDefault_OS());
            defaultParameters.Add(CheckDefaultCount());
            foreach(string n in CheckDefaultSoftware())
            {
                defaultParameters.Add(n);
            }*/
            CheckDefaultProcessor();
            CheckDefault_RAM();
            CheckDefault_HDD();
            CheckDefault_OS();
            CheckDefaultCount();
            CheckDefaultSoftware();
            //CheckPriceColor_RED();
        }
    }
}
