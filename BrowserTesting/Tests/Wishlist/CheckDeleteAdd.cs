using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting.Wishlist
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckDeleteAdd :TestBase
    {
        protected PageExplorer explorer;
        protected OrderPage order;
        protected CartPage cart;
        protected WishlistPage wishlist;
        protected string itemName;
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
        public CheckDeleteAdd(string item1)
        {
            itemName = item1;
        }
        [AutomatedTest(7)]
        [Test, Description("Checking the simultaneous deletion and addition of an item to the cart from the Wishlist page via the CheckBox"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info("Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
                    test.Info("Предмет добавлен в Wishlist");
            explorer.OpenWishlist();
            wishlist.RemoveItem(itemName)
                .AddToCart_CheckBox_On(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
                    test.Info("Предмет добавлен в Cart");
                    test.Info("Предмет удален из  Wishlist");
            explorer.OpenCart();
                    test.Info("Открыта страница корзины");
                    test.Info("Проверка наличия предмета в корзине");
            cart.CheckCartItem(itemName);
            cart.RemoveAllItems();
            cart.UpdateCart();
                    test.Info("Корзина очищена");
                    test.Pass("Тест завершен");
        }
    }
}
