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
        public string CheckDefaultProcessor()
        {
            SelectElement list = new SelectElement(ProcessorList());
            string nameProcessor = list.SelectedOption.Text;
            return nameProcessor;
        }
        public string CheckDefault_RAM()
        {
            SelectElement list = new SelectElement(RAM_List());
            string nameRAM = list.SelectedOption.Text;
            return nameRAM;
        }
        public ReadOnlyCollection<IWebElement> HDD_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        public string CheckDefault_HDD()
        {
            string nameHDD = "Значение по умолчанию для HDD не установлено";
            int count = 1;
            foreach (IWebElement parameter in HDD_Buttons())
            {
                if (parameter.Selected)
                {
                    nameHDD = parameter.FindElement(
                        By.XPath("(//dl//dt//label[normalize-space(text())='HDD']//../following::dd[1]//li/label)[{count}]"))
                        .Text;
                }
                count++;
            }
            return nameHDD;
        }
        public ReadOnlyCollection<IWebElement> OS_Buttons()
        {
            string path = "//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        public string CheckDefault_OS()
        {
            string nameOS = "Значение по умолчанию для OS не установлено";
            int count = 1;
            foreach (IWebElement parameter in OS_Buttons())
            {
                if (parameter.Selected)
                {
                    nameOS = parameter.FindElement(By.XPath
                        ($"(//dl//dt//label[normalize-space(text())='OS']//../following::dd[1]//li/label)[{count}]"))
                        .Text;
                }
                count++;
            }
            return nameOS;
        }
        public ReadOnlyCollection<IWebElement> SoftwareList()
        {
            string path = "//dl//dt//label[normalize-space(text())='Software']//../following::dd[1]//li/input";
            ReadOnlyCollection<IWebElement> list = Driver.FindElements(By.XPath(path));
            return list;
        }
        public List<string> CheckDefaultSoftware()
        {
            List<string> defaultSoftware = new List<string>();
            int count = 1;
            foreach(IWebElement parameter in SoftwareList())
            {
                if (parameter.Selected)
                {
                    defaultSoftware.Add(Driver.FindElement
                        (By.XPath($"(//dl//dt//label[normalize-space(text())='Software']//../following::dd[1]//li/label)[{count}]"))
                        .Text);
                }
            }
            return defaultSoftware;
        }
        public string CheckDefaultCount()
        {
            SetItemCountField();
            string itemCount = Driver.FindElement(itemCountField).GetAttribute("value");
            return itemCount;
        }
        public bool CheckPriceColor_RED()
        {
            String buttonTextColor = Driver.FindElement(By.XPath("//span[@itemprop='price']")).GetCssValue("color");
            Assert.AreEqual(Colors.Red, buttonTextColor, "Цвет цены предмета не красный");
            return buttonTextColor == Colors.Red;
        }
        public void StartCheckDefaultParameters()
        {
            List<string> defaultParameters = new List<string>();
            defaultParameters.Add(CheckDefaultProcessor());
            defaultParameters.Add(CheckDefault_RAM());
            defaultParameters.Add(CheckDefault_HDD());
            defaultParameters.Add(CheckDefault_OS());
            defaultParameters.Add(CheckDefaultCount());
            foreach(string n in CheckDefaultSoftware())
            {
                defaultParameters.Add(n);
            }
            CheckPriceColor_RED();
        }
    }
}
