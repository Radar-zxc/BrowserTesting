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
            string item1 = "Black & White Diamond Heart";
            string item2 = "Computing and Internet";
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(item1);
            order.CreatePage();
            order.AddItemToCart();
            explorer.OpenCart();
            cart.CheckCartItem(item1);
            explorer.OpenPageInNewTab("books");
            explorer.GoToItemPage(item2);
            order.AddItemToCart();
            explorer.GoToTab(0);
            cart.CheckCartItemAbsence(item2);
            cart.UpdateCart();
            cart.CheckCartItem(item1);
            cart.CheckCartItem(item2);
            cart.RemoveItem(item1);
            cart.RemoveItem(item2);
            cart.UpdateCart();
            cart.CheckEmptyCart();
            explorer.OpenStartPage();
        }
    }
}
