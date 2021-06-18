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
            explorer.OpenPage("books");
            preOrder.AddItem("Computing and Internet");
            explorer.OpenCart();
            cart.StartCheckout();
            checkout.CheckoutAsGuest()
                .WriteBillingInfo()
                .WriteShippingInfo()
                .ChooseShipping()
                .ChoosePayment()
                .PaymentInfonmation()
                .ConfirmOrder();
            explorer.OpenStartPage();
        }
    }
}
