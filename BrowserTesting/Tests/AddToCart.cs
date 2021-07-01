using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    [TestFixture]
    class AddToCart : TestBase
    {
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }

        private CartPage cart;
        private OrderPage order;
        private PageExplorer explorer;
        [OneTimeSetUp]
        public void Prepare()
        {
            cart = new CartPage(Driver);
            order = new OrderPage(Driver);
            explorer = new PageExplorer(Driver);
        }

        [Test, Description("Add jewelry to cart"), Order(0)]
        public void AddJewelry_MultiplyInCart()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string itemName = "Black & White Diamond Heart";
                    test.Info($"Открыта базовая страница");
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.CreatePage();
                    test.Info($"Добавление предмета {itemName} в корзину");
            order.AddItemToCart();
                    test.Info($"Открытие страницы корзины");
            explorer.OpenCart();
                    test.Info($"Изменение количества предмета {itemName} в корзине ");
            cart.ChangeCount(itemName,50);
            cart.UpdateCart();
                    test.Info($"Проверка цены предмета {itemName} в корзине ");
            cart.CheckPrice(itemName);
                    test.Info($"Удаление предмета {itemName} из корзины ");
            cart.RemoveItem(itemName);
            cart.UpdateCart();
                    test.Info($"Проверка корзины на пустоту");
            cart.CheckEmptyCart();
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Clear cart - add many items in item page"), Order(1)]
        public void AddJewelry_MultiplyInItemPage()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            string itemName = "Black & White Diamond Heart";
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.CreatePage();
                    test.Info($"Изменение количества у предмета {itemName} на его странице информации");
            order.ChangeItemCount(60);
                    test.Info($"Добавление предмета {itemName} в корзину");
            order.AddItemToCart();
            explorer.OpenCart();
            explorer.CheckCartTravel();
                    test.Info($"Проверка стоимости предмета {itemName} в корзине с учетом количества");
            cart.CheckCartItem(itemName);
            cart.CheckPrice(itemName);
                    test.Info($"Удаление предмета {itemName} из корзины");
            cart.RemoveItem(itemName);
            cart.UpdateCart();
            cart.CheckEmptyCart();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
