using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

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
            double itemPrice = double.Parse(Driver.EFindElement(By.XPath(pathItemPrice)).Text.Replace('.', ','));
            return itemPrice;
        }
        /// <summary>
        /// Метод, возвращающий значение итоговой цены предмета в корзине по заданному имени
        /// ввиде числа с плавающей точкой
        /// <summary>
        public double GetTotalPrice(string itemName)
        {
            string pathItemTotalPrice = $"//a[text()='{itemName}']/../..//span[@class='product-subtotal']";
            double itemTotalPrice = double.Parse(Driver.EFindElement(By.XPath(pathItemTotalPrice)).Text.Replace('.',','));
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
            Assert.IsTrue(Driver.EFindElement(By.XPath(path)).Displayed, "Корзина не очищена");
        }
        /// <summary>
        /// Метод, проверяющий наличие предмета в корзине по заданному имени,
        /// предусмотрен Assert при его отсутствии
        /// </summary>
        public void CheckCartItem(string itemName)
        {
            var findItem = Driver.EFindElement(By.XPath($"//tr[@class='cart-item-row']//td[@class='product']//a[text()='{itemName}']"));
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
            int itemCount = Int32.Parse(Driver.EFindElement(SetCountField(itemName)).GetAttribute("value"));
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
        /// Метод активирования CheckBox remove у всех предметов в корзине
        /// </summary>
        public void RemoveAllItems()
        {
            var a = Driver.FindElements(By.Name("removefromcart"));
            foreach (IWebElement i in a)
            {
                Driver.EClick(i);
            }
        }
        /// <summary>
        /// Метод изменения количества предметов в корзине по заданному имени предмета
        /// </summary>
        public void ChangeCount(string itemName, int count)
        {
            By field = SetCountField(itemName);
            Driver.EFindElement(field).Clear();
            Driver.EFindElement(field).ESendKeys(Convert.ToString(count));
        }
        /// <summary>
        /// Метод, начинающий оформление заказа
        /// </summary>
        public void StartCheckout()
        {
            ClickOnElement(By.CssSelector("#termsofservice[type='checkbox']"));
            ClickOnElement(By.CssSelector("#checkout.button-1"));
        }
        /// <summary>
        /// Метод проверки соответствия информации у предмета, той, которая была введена при добавлении в корзину
        /// </summary>
        public void CheckCardInfo(GiftCardInputInfo info)
        {
            var note = Driver.FindElement(By.XPath("//tr[@class='cart-item-row']//a[text()='$25 Virtual Gift Card']/../..//div[@class='attributes']")).Text.Split("\r\n");
            string sendName = note[0].Substring(note[0].IndexOf(' ')+1, note[0].IndexOf('<')- note[0].IndexOf(' ')-2);
            string sendEmail = note[0].Substring(note[0].IndexOf('<')+1 , note[0].IndexOf('>') - note[0].IndexOf('<') - 1);
            string recName = note[1].Substring(note[1].IndexOf(' ') + 1, note[1].IndexOf('<') - note[1].IndexOf(' ') - 2);
            string recEmail = note[1].Substring(note[1].IndexOf('<') + 1, note[1].IndexOf('>') - note[1].IndexOf('<') - 1);
            Assert.AreEqual(sendName,info.sendName);
            Assert.AreEqual(sendEmail, info.sendEmail);
            Assert.AreEqual(recName, info.recName);
            Assert.AreEqual(recEmail,info.recEmail);
        }
        /// <summary>
        /// Метод нажатия кнопки Edit для предмета с заданным именем 
        /// </summary>
        public void StartEditItem(string itemName)
        {
            ClickOnElement(By.XPath($"//tr[@class='cart-item-row']//a[contains(text(),'{itemName}')]//..//..//a[contains(text(),'Edit')]"));
        }
    }
}
