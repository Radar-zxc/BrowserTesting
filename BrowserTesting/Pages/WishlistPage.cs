using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, предназначенный для обработки информации страницы Wishlist
    /// </summary>
    class WishlistPage : CartPage
    {
        public WishlistPage(IWebDriver Driver) : base(Driver)
        {

        }
        /// <summary>
        /// Метод, проверяющий наличие заданного предмета в Wishlist
        /// </summary>
        public WishlistPage CheckItem(string itemName) 
        {
            CheckCartItem(itemName);
            return this;
        }
        /// <summary>
        /// Метод, проверяющий стоимость заданного предмета в Wishlist
        /// </summary>
        public new WishlistPage CheckPrice(string itemName)
        {
            base.CheckPrice(itemName);
            return this;
        }
        /// <summary>
        /// Метод, изменяющий Qty. заданного предмета в Wishlist
        /// </summary>
        public new WishlistPage ChangeCount(string itemName, int count)
        {
            base.ChangeCount(itemName,count);
            return this;
        }
        /// <summary>
        /// Метод, проверяющий наличие заданного предмета в Wishlist for sharing
        /// </summary>
        public WishlistPage CheckSharing(string itemName)
        {
            CheckItem(itemName);
            return this;
        }
        /// <summary>
        /// Метод, активирующий CheckBox Add to cart заданного предмета в Wishlist
        /// </summary>
        public WishlistPage AddToCart_CheckBox_On(string itemName)
        {
            CheckBox_TurnOn(By.XPath($"//a[normalize-space(text())='{itemName}']//..//..//input[@name='addtocart']"));
            return this;
        }
        /// <summary>
        /// Метод, деактивирующий CheckBox Add to cart заданного предмета в Wishlist
        /// </summary>
        public WishlistPage AddToCart_CheckBox_Off(string itemName)
        {
            CheckBox_TurnOff(By.XPath($"//a[normalize-space(text())='{itemName}']//..//..//input[@name='addtocart']"));
            return this;
        }
        /// <summary>
        /// Метод, нажатия на кнопку Add to cart
        /// </summary>
        public WishlistPage AddToCart()
        {
            ClickOnElement(By.XPath("//input[@class='button-2 wishlist-add-to-cart-button']"));
            return this;
        }
        /// <summary>
        /// Метод, нажатия на кнопку Update wishlist
        /// </summary>
        public WishlistPage UpdateWishlist()
        {
            ClickOnElement(By.Name("updatecart"));
            return this;
        }
        /// <summary>
        /// Метод, проверки пустого Wishlist
        /// </summary>
        public WishlistPage CheckEmptyWishlist()
        {
            if (Driver.FindElements(By.XPath("//div[contains(text(),'The wishlist is empty!')]")).Count == 0)
            {
                throw new Exception("Wishlist не очищен");
            }
            return this;
        }
        /// <summary>
        /// Метод, активации CheckBox Remove у предмета с заданным именем
        /// </summary>
        public new WishlistPage RemoveItem(string itemName)
        {
            base.RemoveItem(itemName);
            return this;
        }
    }
}
