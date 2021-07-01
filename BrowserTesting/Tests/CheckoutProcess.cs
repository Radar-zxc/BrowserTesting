using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class CheckoutProcess : TestBase
    {
        private CheckoutPage checkout;
        private PageExplorer explorer;
        private CartPage cart;
        private PreOrderPage preOrder;
        override public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            checkout = new CheckoutPage(Driver);
            explorer = new PageExplorer(Driver);
            cart = new CartPage(Driver);
            preOrder = new PreOrderPage(Driver);
        }
        [Test, Description("Testing of valid checkout process "), Order(0)]
        public void SimpleValidCheckout()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            explorer.OpenPage("books");
                    test.Info($"Добавление предмета Computing and Internet в корзину ");
            preOrder.AddItem("Computing and Internet");
                    test.Info($"Открытие страницы корзины");
            explorer.OpenCart();
            cart.StartCheckout();
                    test.Info($"Проведение Checkout с валидными параметрами ");
            checkout.CheckoutAsGuest()
                .WriteBillingInfo()
                .WriteShippingInfo()
                .ChooseShipping()
                .ChoosePayment()
                .PaymentInfonmation()
                .ConfirmOrder();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
