using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting.Wishlist
{
    [TestFixture("Black & White Diamond Heart")]
    class CheckSharing :TestBase
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
        public CheckSharing(string item1)
        {
            itemName = item1;
        }
        [AutomatedTest(9)]
        [Test, Description("Check Wishlist URL for sharing function"), Order(0)]
        public void StartChecking()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
                    test.Info("Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
                    test.Info("Добавление предмета в Wishlist");
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.CheckItem(itemName);
                    test.Info("Открытие ссылки sharing в новой вкладке");
            explorer.OpenPageInNewTab("share-link");
                    test.Info($"Проверка наличия предмета {itemName} в таблице после перехода по ссылке");
            wishlist.CheckItem(itemName);
                    test.Info($"Закрытие текущей вкладки");
            explorer.CloseCurrentWindow();
                    test.Pass($"Тест завершен");
        }
    }
}
