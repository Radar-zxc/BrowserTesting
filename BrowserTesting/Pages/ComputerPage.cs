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
        public string CheckDefault_HDD()
        {
            //TODO: объеденить radio-button в кластер
            string pathHDD1 = "//input[@id='product_attribute_16_3_6_18']";
            string pathHDD2 = "//input[@id='product_attribute_16_3_6_19']";
            bool checkSelection1 = Driver.FindElement(By.XPath(pathHDD1)).Selected;
            bool checkSelection2 = Driver.FindElement(By.XPath(pathHDD2)).Selected;
            string nameHDD="Значение по умолчанию для HDD не установлено";
            if (checkSelection1)
            {
                nameHDD = Driver.FindElement(By.XPath(pathHDD1)).Text;
            }
            else if (checkSelection2)
            {
                nameHDD = Driver.FindElement(By.XPath(pathHDD2)).Text;
            }
            return nameHDD;
        }
        public string CheckDefault_OS()
        {
            //TODO: объединить radio-button в кластер
            string pathOS1 = "//input[@id='product_attribute_16_4_7_44']";
            string pathOS2 = "//input[@id='product_attribute_16_4_7_20']";
            string pathOS3 = "//input[@id='product_attribute_16_4_7_21']";
            string label = "//..//label";
            bool checkSelection1 = Driver.FindElement(By.XPath(pathOS1)).Selected;
            bool checkSelection2 = Driver.FindElement(By.XPath(pathOS2)).Selected;
            bool checkSelection3 = Driver.FindElement(By.XPath(pathOS3)).Selected;
            string nameOS = "Значение по умолчанию для OS не установлено";
            if (checkSelection1)
            {
                nameOS = Driver.FindElement(By.XPath(pathOS1+label)).Text;
            }
            else if (checkSelection2)
            {
                nameOS = Driver.FindElement(By.XPath(pathOS2 + label)).Text;
            }
            else if (checkSelection3)
            {
                nameOS = Driver.FindElement(By.XPath(pathOS3 + label)).Text;
            }
            return nameOS;
        }
        public List<string> CheckDefaultSoftware()
        {
            //TODO: объединить чеклисты в кластер
            string pathSoftware1 = "//input[@id='product_attribute_16_8_8_22']";
            string pathSoftware2 = "//input[@id='product_attribute_16_8_8_23']";
            string pathSoftware3 = "//input[@id='product_attribute_16_8_8_24']";
            string label = "//..//label";
            List<string> defaultSoftware = new List<string>();
            bool checkSelection1 = Driver.FindElement(By.XPath(pathSoftware1)).Selected;
            bool checkSelection2 = Driver.FindElement(By.XPath(pathSoftware2)).Selected;
            bool checkSelection3 = Driver.FindElement(By.XPath(pathSoftware3)).Selected;
            if (checkSelection1)
            {
                defaultSoftware.Add(Driver.FindElement(By.XPath(pathSoftware1 + label)).Text);
            }
            else if (checkSelection2)
            {
                defaultSoftware.Add(Driver.FindElement(By.XPath(pathSoftware2 + label)).Text);
            }
            else if (checkSelection3)
            {
                defaultSoftware.Add(Driver.FindElement(By.XPath(pathSoftware3 + label)).Text);
            }
            if (defaultSoftware.Count == 0)
            {
                defaultSoftware.Add("Значение по умолчанию для OS не установлено");
            }
            return defaultSoftware;
        }
        //TODO: добавить проверку количества по умолчанию
        //TODO: проверить, что цвет цены предмета КРАСНЫЙ
        public void StartCheckDefaultParameters()
        {
            List<string> defaultParameters = new List<string>();
            defaultParameters.Add(CheckDefaultProcessor());
            defaultParameters.Add(CheckDefault_RAM());
            defaultParameters.Add(CheckDefault_HDD());
            defaultParameters.Add(CheckDefault_OS());
            foreach(string n in CheckDefaultSoftware())
            {
                defaultParameters.Add(n);
            }
        }
    }
}
