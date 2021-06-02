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
using System.Linq;
using System.Reflection.Metadata;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для проверки общей страницы Books
    /// </summary>
    class BooksPage : OrderPage
    {
        private By popupList_Sort = By.Id("products-orderby");
        private By popupList_Sort_A_to_Z = By.XPath("//option[text()='Name: A to Z']");
        private By popupList_Sort_Z_to_A = By.XPath("//option[text()='Name: Z to A']");
        private By popupList_Sort_HighToLow = By.XPath("//option[text()='Price: High to Low']");
        private By popupList_Sort_LowToHigh = By.XPath("//option[text()='Price: Low to High']");
        private By popupList_Sort_Default;
        public BooksPage(IWebDriver Driver) : base(Driver)
        {
            string defaultSort = "Position";
            popupList_Sort_Default = By.XPath($"//option[text()='{defaultSort}']");
        }
        /// <summary>
        /// Метод формирования списка книг на странице по Именам
        /// </summary>
        private List<string> BooksListName()
        {
            string path = "//h2[@class='product-title']";
            List<string> list = new List<string>();
            list = (from i in Driver.FindElements(By.XPath(path))
                select i.Text).ToList();
            return list;
        }
        /// <summary>
        /// Метод формирования списка книг на странице по Цене
        /// </summary>
        private List<string> BooksListPrice()
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
            IWebElement list = Driver.FindElement(By.XPath(path));
            return list;
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки A to Z, 
        /// предусмотрен Assert при несоответствии требованиям
        /// </summary>
        private void CheckSort_A_to_Z()
        {;
            IOrderedEnumerable<string> linqSorted = BooksListName().OrderBy(i => i);
            Assert.AreEqual(linqSorted, BooksListName(),
                "Фактическая сортировка A to Z произведена неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки Z to A, 
        /// предусмотрен Assert при несоответствии требованиям
        /// </summary>
        private void CheckSort_Z_to_A()
        {
            IOrderedEnumerable<string> linqSorted = BooksListName().OrderByDescending(i => i);
            Assert.AreEqual(linqSorted, BooksListName(),
                "Фактическая сортировка Z to A произведена неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки High to Low, 
        /// предусмотрен Assert при несоответствии требованиям 
        /// </summary>
        private void CheckSort_HighToLow()
        {
            IOrderedEnumerable<string> linqSorted = BooksListPrice().OrderByDescending(i => i);
            Assert.AreEqual(linqSorted, BooksListPrice(),
                "Фактическая сортировка High to Low происходит неверно");
        }
        /// <summary>
        /// Метод проверки корректности проведения сортировки Low to High, 
        /// предусмотрен Assert при несоответствии требованиям 
        /// </summary>
        private void CheckSort_LowToHigh()
        {
            IOrderedEnumerable<string> linqSorted = BooksListPrice().OrderBy(i => i);
            Assert.AreEqual(linqSorted, BooksListPrice(),
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
            Assert.AreEqual(Driver.FindElement(popupList_Sort_Default).Text, nameSort,
                "Фактическая сортировка не соответствует требуемой по умолчанию");
        }
        /// <summary>
        /// Метод изменения текущей сортировки на указанную другую
        /// </summary>
        private void ChangeSort(By newSort)
        {
            PickParameterInPopupList(popupList_Sort, newSort);
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.XPath("//h2[@class='product-title']")).Displayed);
        }

        /// <summary>
        /// Главная функция проверки требуемых сортировок на странице Books
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
