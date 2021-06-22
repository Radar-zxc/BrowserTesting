using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace BrowserTesting.Pages
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для работы со страницами поиска
    /// </summary>
    class SearchPage : BasePage
    {
        public SearchPage(IWebDriver Driver) : base(Driver)
        {
        }
        private readonly By SearchKeywordField = By.CssSelector(".search-text");
        private readonly By AdvancedSearchCheckBox = By.Id("As");
        private readonly By CategoryPopUpList = By.Id(@"Cid");
        private readonly By SupCategoriesCheckBox = By.Id("Isc");
        private readonly By ManufacturerPopUpList = By.Id("Mid");
        private readonly By PriceField_From = By.Id("Pf");
        private readonly By PriceField_To = By.Id("Pt");
        private readonly By SearchInDecsriptionCheckBox = By.Id("Sid");
        private readonly By SearchButton = By.CssSelector(".search-button");
        private readonly By SearchField_Header = By.CssSelector(".search-box-text");
        private readonly By SearchButtom_Header = By.CssSelector(".search-box-button");

        /// <summary>
        /// Метод обновления поиска путем нажатия на кнопку Search
        /// </summary>
        public SearchPage UpdateSearch()
        {
            ClickOnElement(SearchButton);
            return this;
        }
        /// <summary>
        /// Метод обновления поиска путем нажатия на кнопку Search в заголовке сайта
        /// </summary>
        public SearchPage UpdateSearch_Header()
        {
            ClickOnElement(SearchButtom_Header);
            return this;
        }
        /// <summary>
        /// Метод добавления текста соответствующего запроса в поле для поиска
        /// </summary>
        public SearchPage NewRequest(string request)
        {
            Driver.EFindElement(SearchKeywordField).Clear();
            Driver.EFindElement(SearchKeywordField).ESendKeys(request);
            return this;
        }
        /// <summary>
        /// Метод добавления текста соответствующего запроса в поле для поиска в заголовке страницы
        /// </summary>
        public SearchPage NewRequest_Header(string request)
        {
            Driver.EFindElement(SearchField_Header).Clear();
            Driver.EFindElement(SearchField_Header).ESendKeys(request);
            return this;
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
        public SearchPage ChangePrice(int priceFrom, int priceTo)
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
        public SearchPage CheckReceivedItems(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count != 0, "В результатах поиска отсутствуют элементы, удовлетворяющие валидному запросу");
            return this;
        }
        /// <summary>
        /// Метод проверки отсутствия найденных предметов по соответствующему запросу
        /// </summary>
        public SearchPage CheckReceivedItems_Absence(string request)
        {
            Assert.IsTrue(GetReceivedItems(request).Count == 0, "В результатах поиска присутствуют элементы, удовлетворяющие невалидному запросу");
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Выбрано
        /// </summary>
        public SearchPage AdvancedSearch_TurnOn()
        {
            CheckBox_TurnOn(AdvancedSearchCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Advanced search в состояние Не выбрано
        /// </summary>
        public SearchPage AdvancedSearch_TurnOff()
        {
            CheckBox_TurnOff(AdvancedSearchCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Выбрано
        /// </summary>
        public SearchPage AutomaticallySearchSubCategories_TurnOn()
        {
            CheckBox_TurnOn(SupCategoriesCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Automatically search sub categories в состояние Не выбрано
        /// </summary>
        public SearchPage AutomaticallySearchSubCategories_TurnOff()
        {
            CheckBox_TurnOff(SupCategoriesCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Выбрано
        /// </summary>
        public SearchPage SearchInDescription_TurnOn()
        {
            CheckBox_TurnOn(SearchInDecsriptionCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра Search in product descriptions в состояние Не выбрано
        /// </summary>
        public SearchPage SearchInDescription_TurnOff()
        {
            CheckBox_TurnOff(SearchInDecsriptionCheckBox);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра из списка Category на выбранный другой
        /// </summary>
        public SearchPage ChangeCategory(string newCategory)
        {
            SelectElement s = new SelectElement(Driver.EFindElement(CategoryPopUpList));
            s.SelectByText(newCategory);
            return this;
        }
        /// <summary>
        /// Метод переключения параметра из списка Manufacturer на выбранный другой
        /// </summary>
        public SearchPage ChangeManufacturer(string newManufacturer)
        {
            SelectElement s = new SelectElement(Driver.EFindElement(ManufacturerPopUpList));
            s.SelectByText(newManufacturer);
            return this;
        }
        /// <summary>
        /// Метод очистки полей цены From и To
        /// </summary>
        public SearchPage ClearPrice()
        {
            Driver.EFindElement(PriceField_From).Clear();
            Driver.EFindElement(PriceField_To).Clear();
            return this;
        }
        /// <summary>
        /// Метод проверки обработки запроса, состоящего менее чем из 3 символов
        /// </summary>
        public SearchPage CheckSmallRequest()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//strong[@class='warning']")).Displayed,
            "Сообщение о недостаточной длине запроса не выведено на экран");
            return this;
        }
        /// <summary>
        /// Метод закрытия всплывающего окна
        /// </summary>
        public SearchPage PopupWindowWithMessageProcessing()
        {
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
            return this;
        }
    }
}
