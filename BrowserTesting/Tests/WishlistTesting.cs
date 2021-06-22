using NUnit.Framework;
using BrowserTesting.Pages;
using System;

namespace BrowserTesting.Tests
{
    [TestFixture ("Black & White Diamond Heart")]
    class WishlistTesting : TestBase
    {
        private PageExplorer explorer;
        private OrderPage order;
        private CartPage cart;
        private WishlistPage wishlist;
        private string itemName;
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
            wishlist.CheckPrice(itemName)
                .ChangeCount(itemName, 10)
                .UpdateWishlist()
                .CheckPrice(itemName);
        }
        [AutomatedTest(9)]
        [Test, Description("Check Wishlist URL for sharing"), Order(2)]
        public void CheckSharing()
        {
            wishlist.CheckItem(itemName);
            explorer.OpenPageInNewTab("share-link");
            wishlist.CheckItem(itemName);
            explorer.CloseCurrentWindow();
        }
        [AutomatedTest(4)]
        [Test, Description("Check move item from Wishlist -> Cart"), Order(3)]
        public void CheckMoving()
        {
            wishlist.AddToCart_CheckBox_On(itemName)
                .AddToCart();
            cart.CheckCartItem(itemName);
            cart.RemoveItem(itemName);
            cart.UpdateCart();
        }
        [AutomatedTest(5)]
        [Test, Description("Check delete item from Wishlist"), Order(4)]
        public void CheckDelete()
        {
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            cart.RemoveAllItems();
            wishlist.UpdateWishlist()
                .CheckEmptyWishlist();
        }
        [AutomatedTest(7)]
        [Test, Description("Simultaneous deletion and addition of item from Wishlist"), Order(5)]
        public void CheckDeleteAdd()
        {
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.RemoveItem(itemName)
                .AddToCart_CheckBox_On(itemName)
                .UpdateWishlist()
                .CheckEmptyWishlist();
            explorer.OpenCart();
            cart.CheckCartItem(itemName);
            cart.RemoveAllItems();
            cart.UpdateCart();
        }
        [AutomatedTest(8)]
        [Test, Description("Simultaneous deletion and addition of item from Wishlist in different tabs"), Order(6)]
        public void CheckDeleteAdd_InDiffTabs()
        {
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
