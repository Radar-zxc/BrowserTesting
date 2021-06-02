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

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для взаимодействия со страницей Cart
    /// </summary>
    class CartPage : BasePage
    {
        public static string cartUrl = "http://demowebshop.tricentis.com/cart";
        public CartPage(IWebDriver Driver) : base(Driver)
        {

        }
        private By updateButton = By.XPath("//input[@class='button-2 update-cart-button']");
        /// <summary>
        /// Метод нахождения локатора CheckBox для удаления предмета из корзины по заданному имени
        /// </summary>
        private By SetRemoveButton(string itemName)
        {
            string path = $"//tr[@class='cart-item-row']//a[normalize-space(text())='{itemName}']//..//..//input[@type='checkbox']";
            By itemRemoveButton = By.XPath(path);
            return itemRemoveButton ;
        }
        /// <summary>
        /// Метод, возвращающий значение цены предмета в корзине по заданному имени
        /// ввиде числа с плавающей точкой
        /// </summary>
        public double GetPrice(string itemName)
        {
            string pathItemPrice = $"//a[text()='{itemName}']/../..//span[@class='product-unit-price']";
            double itemPrice = double.Parse(Driver.FindElement(By.XPath(pathItemPrice)).Text.Replace('.', ','));
            return itemPrice;
        }
        /// <summary>
        /// Метод, возвращающий значение итоговой цены предмета в корзине по заданному имени
        /// ввиде числа с плавающей точкой
        /// <summary>
        public double GetTotalPrice(string itemName)
        {
            string pathItemTotalPrice = $"//a[text()='{itemName}']/../..//span[@class='product-subtotal']";
            double itemTotalPrice = double.Parse(Driver.FindElement(By.XPath(pathItemTotalPrice)).Text.Replace('.',','));
            return itemTotalPrice;
        }
        /// <summary>
        /// Метод нахождения локатора поля, содержащего количество предметов в корзине по заданному имени
        /// </summary>
        private By SetCountField(string itemName)
        {
            string pathItemCount = $"//a[text()='{itemName}']/../..//input[@class='qty-input']";
            By itemCountField = By.XPath(pathItemCount);
            return itemCountField;
        }
        /// <summary>
        /// Метод, проверяющий итоговую цену предмета в корзине, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void CheckPrice(string itemName)
        {
            Assert.AreEqual(GetTotalPrice(itemName), GetPrice(itemName) * GetItemCount(itemName), "Вычисление итоговой суммы произведено неверно");
        }
        /// <summary>
        /// Метод, проверяющий, что корзина пуста, 
        /// предусмотрен Assert при несоответствии требованию
        /// </summary>
        public void CheckEmptyCart()
        {
            string path = "//div[@class='page-body']//div[normalize-space(text())='Your Shopping Cart is empty!']";
            Assert.IsTrue(Driver.FindElement(By.XPath(path)).Displayed, "Корзина не очищена");
        }
        /// <summary>
        /// Метод, проверяющий наличие предмета в корзине по заданному имени,
        /// предусмотрен Assert при его отсутствии
        /// </summary>
        public void CheckCartItem(string itemName)
        {
            var findItem = Driver.FindElement(By.XPath($"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']"));
            Assert.IsTrue(findItem.Displayed, $"Предмет {itemName} отсутствует в корзине ");
        }
        /// <summary>
        /// Метод, проверяющий отсутствие предмета в корзине по заданному имени, 
        /// выброс исключения при его наличии
        /// </summary>
        public void CheckCartItemAbsence(string itemName)
        {
            var findItem = Driver.FindElements(By.XPath
                ($"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']")).Count;
            if (findItem != 0)
            {
                throw new Exception($"Предмет {itemName} присутствует в корзине ");
            }
        }
        /// <summary>
        /// Метод получения целочисленного значения количества предметов в корзине по заданному имени 
        /// </summary>
        public int GetItemCount(string itemName)
        {
            int itemCount = Int32.Parse(Driver.FindElement(SetCountField(itemName)).GetAttribute("value"));
            return itemCount;
        }
        /// <summary>
        /// Метод обновляющий страницу Cart, при помощи нажатия соответствующей кнопки на странице
        /// </summary>
        public void UpdateCart()
        {
            ClickOnElement(updateButton);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
        /// <summary>
        /// Метод нажатия на кнопку Remove у предмета с заданным именем
        /// </summary>
        public void RemoveItem(string itemName)
        {
            ClickOnElement(SetRemoveButton(itemName));
        }
        /// <summary>
        /// Метод изменения количества предметов в корзине по заданному имени предмета
        /// </summary>
        public void ChangeCount(string itemName, int count)
        {
            By field = SetCountField(itemName);
            Driver.FindElement(field).Clear();
            Driver.FindElement(field).SendKeys(Convert.ToString(count));
        }
    }
}
