using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class SwitchAndUpdatePage : TestBase
    {
        private PageExplorer explorer;
        private OrderPage order;
        private CartPage cart;
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
        }
        [Test, Description("Сhecking the synchronization of tabs"), Order(0)]
        public void СheckingSynchronization()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            string item1 = "Black & White Diamond Heart";
            string item2 = "Computing and Internet";
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(item1);
            order.CreatePage();
                    test.Info($"Добавление предмета {item1} в корзину");
            order.AddItemToCart();
                    test.Info($"Открытие страницы корзины");
            explorer.OpenCart();
            cart.CheckCartItem(item1);
                    test.Info($"Открытие категории Books в новой вкладке");
            explorer.OpenPageInNewTab("books");
            explorer.GoToItemPage(item2);
                    test.Info($"Добавление предмета {item2} в корзину");
            order.AddItemToCart();
                    test.Info($"Переход в первую вкладку");
            explorer.GoToTab(0);
                    test.Info($"Проверка отсутствия предмета {item2} в корзине");
            cart.CheckCartItemAbsence(item2);
                    test.Info($"Обновление корзины");
                    test.Info($"Проверка наличия предметов {item1} и {item2} в корзине");
            cart.UpdateCart();
            cart.CheckCartItem(item1);
            cart.CheckCartItem(item2);
                    test.Info($"Удаление предметов {item1} и {item2} из корзины");
            cart.RemoveItem(item1);
            cart.RemoveItem(item2);
            cart.UpdateCart();
            cart.CheckEmptyCart();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
