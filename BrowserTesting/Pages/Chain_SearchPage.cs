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
    class Chain_SearchPage : BasePage
    {
        public Chain_SearchPage(IWebDriver Driver) : base(Driver)
        {
        }
        private readonly By SearchKeywordField = By.XPath("//input[@class='search-text']");
        private readonly By AdvancedSearchCheckBox = By.XPath("//input[@id='As']");
        private readonly By CategoryPopUpList = By.XPath("//select[@id='Cid']");
        private readonly By SupCategoriesCheckBox = By.XPath("//input[@id='Isc']");
        private readonly By ManufacturerPopUpList = By.XPath("//select[@id='Mid']");
        private readonly By PriceField_From = By.XPath("//input[@id='Pf']");
        private readonly By PriceField_To = By.XPath("//input[@id='Pt']");
        private readonly By SearchInDecsriptionCheckBox = By.XPath("//input[@id='Sid']");
        private readonly By SearchButton = By.XPath("//input[@class='button-1 search-button']");
        /// <summary>
        /// Метод обновления поиска путем нажатия на кнопку Search
        /// </summary>
        public Chain_SearchPage UpdateSearch()
        {
            Driver.FindElement(SearchButton).Click();
            return this;
        }
        /// <summary>
        /// Метод добавления текста соответствующего запроса в поле для поиска
        /// </summary>
        public Chain_SearchPage NewRequest(string request)
        {
            Driver.FindElement(SearchKeywordField).Clear();
            Driver.FindElement(SearchKeywordField).SendKeys(request);
            return this;
        }
        /// <summary>
        /// Переключение CheckBox в состояние Выбрано
        /// </summary>
        private void CheckBox_TurnOn(By checkBox)
        {
            IWebElement elem = Driver.FindElement(checkBox);
            if (!elem.Selected)
            {
                elem.Click();
            }
        }
        /// <summary>
        /// Переключение CheckBox в состояние Не выбрано
        /// </summary>
        private void CheckBox_TurnOff(By checkBox)
        {
            IWebElement elem = Driver.FindElement(checkBox);
            if (elem.Selected)
            {
                elem.Click();
            }
        }
        /// <summary>
        /// Метод создания списка из параметров Manufacturer
        /// </summary>
        private List<string> ListManufacturer()
        {
            List<string> list = new List<string>();
            list.Add(Driver.FindElement(By.XPath("//select[@id='Mid']//option[text()='All']")).Text);
            list.Add(Driver.FindElement(By.XPath("//select[@id='Mid']//option[text()='Tricentis']")).Text);
            return list;
        }
        /// <summary>
        /// Метод изменения цены в поле From на новую
        /// </summary>
        private void ChangePrice_From(int newPrice)
        {
            ChangeCount(PriceField_From, newPrice);
        }
        /// <summary>
        /// Метод изменения цены в поле To на новую
        /// </summary>
        private void ChangePrice_To(int newPrice)
        {
            ChangeCount(PriceField_To, newPrice);
        }
        /// <summary>
        /// Метод изменения цен в полях From и To
        /// </summary>
        public Chain_SearchPage ChangePrice(int priceFrom, int priceTo)
        {
            ChangePrice_From(priceFrom);
            ChangePrice_To(priceTo);
            return this;
        }
        /// <summary>
        /// Метод составления списка найденных предметов по соответствующему запросу
        /// </summary>
        private List<IWebElement> GetReceivedItems(string request)
        {
            List<IWebElement> list = new List<IWebElement>();
            list = (from item in Driver.FindElements(By.XPath($"//div[@class='details']//a[contains(a,{request})]"))
                    select item).ToList();
            return list;
        }
        /// <summary>
        /// Метод проверки наличия найденных предметов по соответствующему запросу
        /// </summary>
        public Chain_SearchPage CheckReceivedItems(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count != 0, "В результатах поиска отсутствуют элементы, удовлетворяющие валидному запросу");
            return this;
        }
        /// <summary>
        /// Метод проверки отсутствия найденных предметов по соответствующему запросу
        /// </summary>
        public Chain_SearchPage CheckReceivedItems_Absence(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count == 0, "В результатах поиска присутствуют элементы, удовлетворяющие невалидному запросу");
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Выбрано
        /// </summary>
        public Chain_SearchPage AdvancedSearch_TurnOn()
        {
            CheckBox_TurnOn(AdvancedSearchCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Не выбрано
        /// </summary>
        public Chain_SearchPage AdvancedSearch_TurnOff()
        {
            CheckBox_TurnOff(AdvancedSearchCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Выбрано
        /// </summary>
        public Chain_SearchPage AutomaticallySearchSubCategories_TurnOn()
        {
            CheckBox_TurnOn(SupCategoriesCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Не выбрано
        /// </summary>
        public Chain_SearchPage AutomaticallySearchSubCategories_TurnOff()
        {
            CheckBox_TurnOff(SupCategoriesCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Выбрано
        /// </summary>
        public Chain_SearchPage SearchInDescription_TurnOn()
        {
            CheckBox_TurnOn(SearchInDecsriptionCheckBox);
            return this; 
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Не выбрано
        /// </summary>
        public Chain_SearchPage SearchInDescription_TurnOff()
        {
            CheckBox_TurnOff(SearchInDecsriptionCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра из списка Category на выбранный другой
        /// </summary>
        public Chain_SearchPage ChangeCategory(string newCategory)
        {
            SelectElement s = new SelectElement(Driver.FindElement(CategoryPopUpList));
            s.SelectByText(newCategory);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра из списка Manufacturer на выбранный другой
        /// </summary>
        public Chain_SearchPage ChangeManufacturer(string newManufacturer)
        {
            SelectElement s = new SelectElement(Driver.FindElement(ManufacturerPopUpList));
            s.SelectByText(newManufacturer);
            return this;
        }
        /// <summary>
        /// Метод очистки полей цены From и To
        /// </summary>
        public Chain_SearchPage ClearPrice()
        {
            Driver.FindElement(PriceField_From).Clear();
            Driver.FindElement(PriceField_To).Clear();
            return this;
        }
        /// <summary>
        /// Метод проверки обработки запроса, состоящего менее чем из 3 символов
        /// </summary>
        public Chain_SearchPage CheckSmallRequest()
        {
            Assert.IsTrue(Driver.FindElement(By.XPath("//strong[@class='warning']")).Displayed,
                "Сообщение о недостаточной длине запроса не выведено на экран");
            return this;
        }
    }
}
