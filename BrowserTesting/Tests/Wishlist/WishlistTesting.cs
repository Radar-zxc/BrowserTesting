using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting.Wishlist
{
    [TestFixture ("Black & White Diamond Heart")]
    class WishlistTesting : TestBase
    {
        protected PageExplorer explorer;
        protected OrderPage order;
        protected CartPage cart;
        protected WishlistPage wishlist;
        protected string itemName;

        public WishlistTesting(string item1)
        {
            itemName = item1;
        }
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            order = new OrderPage(Driver);
            cart = new CartPage(Driver);
            wishlist = new WishlistPage(Driver);
        }
        [AutomatedTest(3)]
        [Test, Description("Add item to wishlist"), Order(0)]
        public void AddToWishlist()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info("Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
                    test.Info($"Добавление предмета {itemName} в Wishlist");
            order.AddItemToWishlist();
            explorer.OpenWishlist();
                    test.Info($"Проверка наличия предмета {itemName} в Wishlist");
            wishlist.CheckItem(itemName);
                    test.Pass($"Тест завершен");
        }
        [AutomatedTest(6)]
        [Test, Description("Check item price in wishlist"), Order(1)]
        public void CalculatePrice()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info($"Проверка цены предмета {itemName} в Wishlist");
            wishlist.CheckPrice(itemName)
                .ChangeCount(itemName, 10)
                .UpdateWishlist()
                .CheckPrice(itemName);
                    test.Pass($"Тест завершен");
        }
        [AutomatedTest(4)]
        [Test, Description("Check move item from Wishlist -> Cart"), Order(2)]
        public void CheckMoving()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info($"Добавление предмета {itemName} из Wishlist в корзину");
            wishlist.AddToCart_CheckBox_On(itemName)
                .AddToCart();
                    test.Info($"Проверка наличия предмета {itemName} в корзине");
            cart.CheckCartItem(itemName);
                    test.Info($"Удаление предмета {itemName} из корзины");
            cart.RemoveItem(itemName);
            cart.UpdateCart();
                    test.Pass($"Тест завершен");
        }
        [AutomatedTest(5)]
        [Test, Description("Check delete item from Wishlist"), Order(3)]
        public void CheckDelete()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
                    test.Info($"Добавление предмета {itemName} в Wishlist");
            order.AddItemToWishlist();
            explorer.OpenWishlist();
                    test.Info($"Удаление всех предметов из списка Wishlist");
            wishlist.RemoveAllItems();
                    test.Info($"Проверка результата удаления");
            wishlist.UpdateWishlist()
                .CheckEmptyWishlist();
                    test.Pass($"Тест завершен");
        }
    }
}
