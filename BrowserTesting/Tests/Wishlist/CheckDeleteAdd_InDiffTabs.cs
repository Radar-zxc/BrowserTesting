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
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            explorer.OpenPageInNewTab("wishlist");
            wishlist.RemoveItem(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
            explorer.GoToTab(0);
            wishlist.AddToCart_CheckBox_On(itemName)
                .UpdateWishlist();
            explorer.CheckCartTravel();
            cart.CheckCartItem(itemName);
            cart.RemoveAllItems();
            explorer.CloseWindow(1);
        }
    }
}
