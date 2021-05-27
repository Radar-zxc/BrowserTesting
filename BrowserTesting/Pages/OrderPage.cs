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
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для обработки страницы товара
    /// </summary>
    class OrderPage : BasePage
    {
        public string itemName;
        public OrderPage (IWebDriver Driver):base(Driver)
        {

        }
        protected By itemPrice;
        protected By itemAddButton;
        protected By itemCountField;
        /// <summary>
        /// Метод записи локатора для кнопки добавления товара
        /// </summary>
        protected void SetItemAddButton()
        {
            string path = "//div[@class='center-2']//input[@type='button']";
            itemAddButton = By.XPath(path);
        }
        /// <summary>
        /// Метод записи локатора для поля количества товаров
        /// </summary>
        protected void SetItemCountField()
        {
            string path = "//div[@class='center-2']//input [@class='qty-input']";
            itemCountField = By.XPath(path);
        }
        /// <summary>
        /// Метод, возвращающий значение цены предмета в виде числа с плавающей точкой
        /// </summary>
        protected double SetItemValuePrice()
        {
            string path = "//span[@itemprop='price']";
            double price = double.Parse(Driver.FindElement(By.XPath(path)).Text);
            return price;
        }
        /// <summary>
        /// Метод записи локатора для цены предмета
        /// </summary>
        protected void SetItemPrice()
        {
            itemPrice = By.XPath("//span[@itemprop='price']");
        }
        /// <summary>
        /// Метод изменения количества предметов на заданное
        /// </summary>
        public void ChangeItemCount(int count)
        {
            ChangeCount(itemCountField, count);
        }
        /// <summary>
        /// Метод добавления предмета в корзину
        /// </summary>
        public void AddItemToCart()
        {
            AddItem(itemAddButton);
        }
        /// <summary>
        /// Метод определения основных локаторов на странице заказа
        /// </summary>
        public void CreatePage()
        {
            SetItemAddButton();
            SetItemCountField();
            SetItemPrice();
        }
    }
}
