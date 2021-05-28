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

namespace BrowserTesting.Pages
{
    class SearchPage : BasePage
    {
        public SearchPage(IWebDriver Driver) : base(Driver)
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
        private readonly By SearchField_Header = By.XPath("//input[@id='small-searchterms']");
        private readonly By SearchButtom_Header = By.XPath("//input[@class='button-1 search-box-button']");

        /// <summary>
        /// Метод обновления поиска путем нажатия на кнопку Search
        /// </summary>
        public void UpdateSearch()
        {
            Driver.FindElement(SearchButton).Click();
        }
        /// <summary>
        /// Метод обновления поиска путем нажатия на кнопку Search в заголовке сайта
        /// </summary>
        public void UpdateSearch_Header()
        {
            Driver.FindElement(SearchButtom_Header).Click();
        }
        /// <summary>
        /// Метод добавления текста соответствующего запроса в поле для поиска
        /// </summary>
        public void NewRequest(string request)
        {
            Driver.FindElement(SearchKeywordField).Clear();
            Driver.FindElement(SearchKeywordField).SendKeys(request);
        }
        /// <summary>
        /// Метод добавления текста соответствующего запроса в поле для поиска в заголовке страницы
        /// </summary>
        public void NewRequest_Header(string request)
        {
            Driver.FindElement(SearchField_Header).Clear();
            Driver.FindElement(SearchField_Header).SendKeys(request);
        }
        /// <summary>
        /// Переключение CheckBox в состояние Выбрано
        /// </summary>
        public void CheckBox_TurnOn(By checkBox)
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
        public void CheckBox_TurnOff(By checkBox)
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
        public List<string> ListManufacturer()
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
        public void ChangePrice(int priceFrom , int priceTo)
        {
            ChangePrice_From(priceFrom);
            ChangePrice_To(priceTo);
        }
        /// <summary>
        /// Метод составления списка найденных предметов по соответствующему запросу
        /// </summary>
        public List<IWebElement> GetReceivedItems(string request)
        {
            List<IWebElement> list = new List<IWebElement>();
            list = (from item in Driver.FindElements(By.XPath($"//div[@class='details']//a[contains(a,{request})]"))
                   select item).ToList();
            return list;
        }
        /// <summary>
        /// Метод проверки наличия найденных предметов по соответствующему запросу
        /// </summary>
        public void CheckReceivedItems(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count != 0, "В результатах поиска отсутствуют элементы, удовлетворяющие валидному запросу");
        }
        /// <summary>
        /// Метод проверки отсутствия найденных предметов по соответствующему запросу
        /// </summary>
        public void CheckReceivedItems_Absence(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count == 0, "В результатах поиска присутствуют элементы, удовлетворяющие невалидному запросу");
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Выбрано
        /// </summary>
        public void AdvancedSearch_TurnOn()
        {
            CheckBox_TurnOn(AdvancedSearchCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Не выбрано
        /// </summary>
        public void AdvancedSearch_TurnOff()
        {
            CheckBox_TurnOff(AdvancedSearchCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Выбрано
        /// </summary>
        public void AutomaticallySearchSubCategories_TurnOn()
        {
            CheckBox_TurnOn(SupCategoriesCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Не выбрано
        /// </summary>
        public void AutomaticallySearchSubCategories_TurnOff()
        {
            CheckBox_TurnOff(SupCategoriesCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Выбрано
        /// </summary>
        public void SearchInDescription_TurnOn()
        {
            CheckBox_TurnOn(SearchInDecsriptionCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Не выбрано
        /// </summary>
        public void SearchInDescription_TurnOff()
        {
            CheckBox_TurnOff(SearchInDecsriptionCheckBox);
        }
        /// <summary>
        /// Метод переключения параметра из списка Category на выбранный другой
        /// </summary>
        public void ChangeCategory(string newCategory)
        {
            SelectElement s = new SelectElement(Driver.FindElement(CategoryPopUpList));
            s.SelectByText(newCategory);
        }
        /// <summary>
        /// Метод переключения параметра из списка Manufacturer на выбранный другой
        /// </summary>
        public void ChangeManufacturer(string newManufacturer)
        {
            SelectElement s = new SelectElement(Driver.FindElement(ManufacturerPopUpList));
            s.SelectByText(newManufacturer);
        }
        /// <summary>
        /// Метод очистки полей цены From и To
        /// </summary>
        public void ClearPrice()
        {
            Driver.FindElement(PriceField_From).Clear();
            Driver.FindElement(PriceField_To).Clear();
        }
        /// <summary>
        /// Метод проверки обработки запроса, состоящего менее чем из 3 символов
        /// </summary>
        public void CheckSmallRequest()
        {
            Assert.IsTrue(Driver.FindElement(By.XPath("//strong[@class='warning']")).Displayed,
                "Сообщение о недостаточной длине запроса не выведено на экран");
        }
        /// <summary>
        /// Метод закрытия всплывающего окна
        /// </summary>
        public void PopupWindowWithMessageProcessing()
        {
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
        }
    }
}
