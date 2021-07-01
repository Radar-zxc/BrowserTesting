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
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.CheckItem(itemName);
        }
        [AutomatedTest(6)]
        [Test, Description("Check item price in wishlist"), Order(1)]
        public void CalculatePrice()
        {  
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            wishlist.CheckPrice(itemName)
                .ChangeCount(itemName, 10)
                .UpdateWishlist()
                .CheckPrice(itemName);
        }
        [AutomatedTest(4)]
        [Test, Description("Check move item from Wishlist -> Cart"), Order(2)]
        public void CheckMoving()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            wishlist.AddToCart_CheckBox_On(itemName)
                .AddToCart();
            cart.CheckCartItem(itemName);
            cart.RemoveItem(itemName);
            cart.UpdateCart();
        }
        [AutomatedTest(5)]
        [Test, Description("Check delete item from Wishlist"), Order(3)]
        public void CheckDelete()
        {
            test = extent.CreateTest("Test-case №" + AutomatedTestAttribute.value + '\n' + DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.RemoveAllItems();
            wishlist.UpdateWishlist()
                .CheckEmptyWishlist();
        }
    }
}
