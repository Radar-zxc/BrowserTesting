using NUnit.Framework;
using BrowserTesting.Pages;
using System;

namespace BrowserTesting.Tests.Wishlist
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
            explorer.OpenPage("jewelry");
            explorer.GoToItemPage(itemName);
            order.AddItemToWishlist();
            explorer.OpenWishlist();
            wishlist.CheckItem(itemName);
            explorer.OpenPageInNewTab("share-link");
            wishlist.CheckItem(itemName);
            explorer.CloseCurrentWindow();
        }
    }
}
