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
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для обработки страницы Build your own computer
    /// </summary>
    class ComputerPage : OrderPage
    {
        private readonly string defaultProcessor = "2.5 GHz Intel Pentium Dual-Core E2200 [+15.00]";
        private readonly string defaultRAM = "2 GB";
        private readonly string defaultHDD = "";
        private readonly string defaultOS = "Ubuntu";
        private readonly string[] defaultSoftware = new string[] {"Microsoft Office [+50.00]" };
        private readonly string defaultCount = "1";
        private readonly string defaultPriceColor = Colors.Red;
        private readonly string defaultPrice = "1200.00";
        public ComputerPage(IWebDriver Driver) : base(Driver)
        {

        }
        /// <summary>
        /// Метод поиска и формирования списка доступных элементов выпадающего списка при Processor
        /// </summary>
        private IWebElement ProcessorList()
        {
            string path = "//div[@class='attributes']//select[@id='product_attribute_16_5_4']";
            IWebElement list = Driver.EFindElement(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод поиска и формирования списка доступных элементов выпадающего списка при RAM
        /// </summary>
        private IWebElement RAM_List()
        {
            string path = "//div[@class='attributes']//select[@id='product_attribute_16_6_5']";
            IWebElement list = Driver.EFindElement(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод соотношения выбранного Processor по умолчанию с тем, что указан в требованиях, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckDefaultProcessor()
        {
            SelectElement list = new SelectElement(ProcessorList());
            string nameProcessor = list.SelectedOption.Text;
            Assert.AreEqual(defaultProcessor, nameProcessor, "Фактический процессор не соответствует требуемому по умолчанию");
        }
        /// <summary>
        /// Метод соотношения выбранной RAM по умолчанию с той, которая указана в требованиях, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckDefault_RAM()
        {
            SelectElement list = new SelectElement(RAM_List());
            string nameRAM = list.SelectedOption.Text;
            Assert.AreEqual(defaultRAM, nameRAM, "Фактическая RAM не соответствует требуемомой по умолчанию");
        }
        /// <summary>
        /// Метод поиска и формирования списка доступных RadioButton для HDD
        /// </summary>
        private ReadOnlyCollection<IWebElement> HDD_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод сопоставления выбранного HDD по умолчанию с тем, что указан в требованиях, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
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
                    nameHDD = Driver.EFindElement(
                        By.XPath($"(//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/label)[{i+1}]"))
                        .Text;
                    Assert.AreEqual(defaultHDD, nameHDD, "Фактический HDD не соответствует требуемому по умолчанию");
                    check = false;
                }
                i++;
            }
        }
        /// <summary>
        /// Метод поиска и формирования списка доступных RadioButton для OS
        /// </summary>
        private ReadOnlyCollection<IWebElement> OS_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод сопоставления выбранной по умолчанию RadioButton для списка OS с указанной в требованиях,
        /// предусмотрен Assert при несоответствии
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
                    nameOS = Driver.EFindElement(
                        By.XPath($"(//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/label)[{i + 1}]"))
                        .Text;
                    Assert.AreEqual(defaultOS, nameOS, "Фактическая OS не соответствует требуемой по умолчанию");
                    check = false;
                }
                i++;
            }
        }
        /// <summary>
        /// Метод поиска и формирования списка доступных параметров для CheckBox у Software
        /// </summary>
        private ReadOnlyCollection<IWebElement> SoftwareList()
        {
            string path = "//dl//dt//label[normalize-space(text())='Software']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод для соотношения выбранных элементов по умолчанию в Checkbox при списке Software с элементами, 
        /// указанными в требованиях
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckDefaultSoftware()
        {
            List<string> listSoftware = new List<string>();
            int count = 1;
            foreach(IWebElement parameter in SoftwareList())
            {
                if (parameter.Selected)
                {
                    listSoftware.Add(Driver.EFindElement
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
        /// Метод проверки соответствия фактического значения Qty по умолчанию Qty, 
        /// указанному в требованиях,
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckDefaultCount()
        {
            SetItemCountField();
            string itemCount = Driver.EFindElement(itemCountField).GetAttribute("value");
            Assert.AreEqual(defaultCount, itemCount,
                "Фактическое значение параметра Qty не соответствует требуемому по умолчанию");
        }
        /// <summary>
        /// Метод проверки соответствия фактического цвета цены товара красному цвету (255, 1, 1, 1), 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckPriceColor_RED()
        {
            String buttonTextColor = Driver.EFindElement(By.XPath("//span[@itemprop='price']")).GetCssValue("color");
            Assert.AreEqual(defaultPriceColor, buttonTextColor, "Цвет цены предмета не красный");
        }
        /// <summary>
        /// Метод проверки соответствия фактической цены на странице цене, указанной в требованиях,
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckPrice()
        {
            SetItemPrice();
            string price = Driver.EFindElement(itemPrice).Text;
            Assert.AreEqual(defaultPrice, price, "Фактическая цена не соответствует требуемой по умолчанию");
        }
        ///<summary>
        ///Главная функция проверки парамаетров по умолчанию 
        ///для страницы "Build your own computer"
        ///</summary>
        public void StartCheckDefaultParameters()
        {
            CheckDefaultProcessor();
            CheckDefault_RAM();
            CheckDefault_HDD();
            CheckDefault_OS();
            CheckDefaultCount();
            CheckDefaultSoftware();
            CheckPriceColor_RED();
            CheckPrice();
        }  
    }
}
