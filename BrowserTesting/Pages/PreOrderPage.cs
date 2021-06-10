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
using System.Linq;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, предназначенный для работы со списком предметов на странице определенной категории
    /// </summary>
    class PreOrderPage : BasePage
    {
        private string CurrentFilter;
        private By popupList_Sort = By.Id("products-orderby");
        private By popupList_Sort_A_to_Z = By.XPath("//option[text()='Name: A to Z']");
        private By popupList_Sort_Z_to_A = By.XPath("//option[text()='Name: Z to A']");
        private By popupList_Sort_HighToLow = By.XPath("//option[text()='Price: High to Low']");
        private By popupList_Sort_LowToHigh = By.XPath("//option[text()='Price: Low to High']");
        private By popupList_Sort_Default;
        public PreOrderPage(IWebDriver Driver) : base(Driver)
        {
            string defaultSort = "Position";
            popupList_Sort_Default = By.XPath($"//option[text()='{defaultSort}']");
        }
        /// <summary>
        /// Метод получения целочисленного значения левой границы цены 
        /// </summary>
        private int GetPrice_From(string range)
        { 
            int price = Convert.ToInt32(range.Remove(range.IndexOf('-')));
            return price;
        }
        /// <summary>
        /// Метод получения целочисленного значения правой границы цены
        /// </summary>
        private int GetPrice_To(string range)
        {
            int price = Convert.ToInt32(range.Remove(0, range.IndexOf('-') + 1));
            return price;
        }
        /// <summary>
        /// Метод формирования списка цен найденных на странице предметов при определенном фильтре цены
        /// </summary>
        private List<int> ConstructItemsPriceList()
        {
            List<int> items = new List<int>();
            items = (from i in ItemsListPrice()
                    select i.IndexOf(' ') == -1
                     ? Convert.ToInt32(Convert.ToDouble(i.Replace('.', ',')))
                     : Convert.ToInt32(Convert.ToDouble(i.Remove(0, i.IndexOf(' ')).Replace('.', ','))))
                     .ToList();
            return items;
        }
        /// <summary>
        /// Метод изменения текущего фильтра цены на заданный другой
        /// </summary>
        public PreOrderPage ChooseFilter(string newFilter)
         {
            ClickOnElement(By.XPath($"//a[contains(@href,'{newFilter}')]"));
            CurrentFilter = newFilter;
            return this;
        }
        /// <summary>
        /// Метод проверки корректности вывода в соответствии с имеющимся фильтром цены
        /// </summary>
        private void CheckFiltering(List<int> items)
        {
            int priceFrom = GetPrice_From(CurrentFilter);
            int priceTo = GetPrice_To(CurrentFilter);
            bool compliance = true;
            foreach (int item in items)
            {
                if (item < priceFrom || item > priceTo)
                {
                    compliance = false;
                }
            }
            Assert.IsTrue(compliance, $"Результат фильтрации {CurrentFilter} не соответствует требованиям");
            RemoveFilter();
        }
        /// <summary>
        /// Метод сброса текущего фильтра цены
        /// </summary>
        private void RemoveFilter()
        {
            ClickOnElement(By.CssSelector(".remove-price-range-filter"));
        }
        /// <summary>
        /// Метод проверки функционирования фильтров цены
        /// </summary>
        public PreOrderPage CheckTable()
        {
            CheckFiltering(ConstructItemsPriceList());
            return this;
        }
        /// <summary>
        /// Метод формирования списка предметов на странице по Именам
        /// </summary>
        private List<string> ItemsListName()
        {
            string path = "//h2[@class='product-title']";
            List<string> list = new List<string>();
            list = (from i in Driver.FindElements(By.XPath(path))
                    select i.Text).ToList();
            return list;
        }
        /// <summary>
        /// Метод формирования списка <string> предметов на странице по Цене
        /// </summary>
        private List<string> ItemsListPrice()
        {
            string path = "//span[@class='price actual-price']";
            List<string> list = new List<string>();
            list = (from i in Driver.FindElements(By.XPath(path))
                    select i.Text).ToList();
            return list;
        }
        /// <summary>
        /// Метод формирования списка возможных сортировок в выпадающем окне Sort by
        /// </summary>
        private IWebElement SortList()
        {
            string path = "//select[@id='products-orderby']";
            IWebElement list = Driver.EFindElement(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки A to Z, 
        /// предусмотрен Assert при несоответствии требованиям
        /// </summary>
        private void CheckSort_A_to_Z()
        {
            ;
            IOrderedEnumerable<string> linqSorted = ItemsListName().OrderBy(i => i);
            Assert.AreEqual(linqSorted, ItemsListName(),
                "Фактическая сортировка A to Z произведена неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки Z to A, 
        /// предусмотрен Assert при несоответствии требованиям
        /// </summary>
        private void CheckSort_Z_to_A()
        {
            IOrderedEnumerable<string> linqSorted = ItemsListName().OrderByDescending(i => i);
            Assert.AreEqual(linqSorted, ItemsListName(),
                "Фактическая сортировка Z to A произведена неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки High to Low, 
        /// предусмотрен Assert при несоответствии требованиям 
        /// </summary>
        private void CheckSort_HighToLow()
        {
            IOrderedEnumerable<string> linqSorted = ItemsListPrice().OrderByDescending(i => i);
            Assert.AreEqual(linqSorted, ItemsListPrice(),
                "Фактическая сортировка High to Low происходит неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки Low to High, 
        /// предусмотрен Assert при несоответствии требованиям 
        /// </summary>
        private void CheckSort_LowToHigh()
        {
            IOrderedEnumerable<string> linqSorted = ItemsListPrice().OrderBy(i => i);
            Assert.AreEqual(linqSorted, ItemsListPrice(),
                "Фактическая сортировка Low to High происходит неверно");
        }
        /// <summary>
        /// Метод проверки соответствия фактической сортировки по умолчанию указанной в требованиях, 
        /// предусмотрен Assert при несоответствии 
        /// </summary>
        private void CheckSort_Default()
        {
            SelectElement sortList = new SelectElement(SortList());
            string nameSort = sortList.SelectedOption.Text;
            Assert.AreEqual(Driver.EFindElement(popupList_Sort_Default).Text, nameSort,
                "Фактическая сортировка не соответствует требуемой по умолчанию");
        }
        /// <summary>
        /// Метод изменения текущей сортировки на указанную другую
        /// </summary>
        private void ChangeSort(By newSort)
        {
            PickParameterInPopupList(popupList_Sort, newSort);
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.EFindElement(By.XPath("//h2[@class='product-title']")).Displayed);
        }
        /// <summary>
        /// Метод проверки сортировок на странице 
        /// </summary>
        public void CheckSort()
        {
            CheckSort_Default();
            ChangeSort(popupList_Sort_A_to_Z);
            CheckSort_A_to_Z();
            ChangeSort(popupList_Sort_Z_to_A);
            CheckSort_Z_to_A();
            ChangeSort(popupList_Sort_HighToLow);
            CheckSort_HighToLow();
            ChangeSort(popupList_Sort_LowToHigh);
            CheckSort_LowToHigh();
        }
    }
}
