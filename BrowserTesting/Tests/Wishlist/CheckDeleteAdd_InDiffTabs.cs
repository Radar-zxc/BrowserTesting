using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting.Wishlist
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckDeleteAdd_InDiffTabs : TestBase
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
        public CheckDeleteAdd_InDiffTabs(string item1)
        {
            itemName = item1;
        }
        [AutomatedTest(8)]
        [Test, Description("Removing one item from the Wishlist and adding it to Cart in different browser tabs"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info("Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
                    test.Info("Предмет добавлен в Wishlist");
            explorer.OpenWishlist();
                    test.Info("Открытие страницы Wishlist в новой вкладке");
            explorer.OpenPageInNewTab("wishlist");
                    test.Info("Удаление предмета из списка Wishlist");
            wishlist.RemoveItem(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
                    test.Info("Переход на первую вкладку");
            explorer.GoToTab(0);
                    test.Info("Добавление предмета в корзину и переход в нее");
            wishlist.AddToCart_CheckBox_On(itemName)
                .UpdateWishlist();
            explorer.CheckCartTravel();
                    test.Info("Проверка наличия предмета в корзине");
            cart.CheckCartItem(itemName);
                    test.Info("Очистка корзины");
            cart.RemoveAllItems();
                    test.Info("Закрытие второго окна");
            explorer.CloseWindow(1);
                    test.Pass("Тест завершен");
        }
    }
}
