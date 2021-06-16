using OpenQA.Selenium;
namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для обработки страницы товара
    /// </summary>
    class OrderPage : BasePage
    {
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
            string path = "input.add-to-cart-button";
            itemAddButton = By.CssSelector(path);
        }
        /// <summary>
        /// Метод записи локатора для поля количества товаров
        /// </summary>
        protected void SetItemCountField()
        {
            string path = "input.qty-input";
            itemCountField = By.CssSelector(path);
        }
        /// <summary>
        /// Метод записи локатора для цены предмета
        /// </summary>
        protected void SetItemPrice()
        {
            itemPrice = By.CssSelector(@".product-price>span[""itemprop=price""]");
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
            ClickOnElement(itemAddButton);
            WaitLoadingCircle();
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
