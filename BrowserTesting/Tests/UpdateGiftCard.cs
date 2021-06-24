using NUnit.Framework;
using BrowserTesting.Pages;
using System;

namespace BrowserTesting
{
    class UpdateGiftCard : TestBase
    {
        private PageExplorer explorer;
        private OrderPage order;
        private CartPage cart;
        private GiftCardPage gift;
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
            gift = new GiftCardPage(Driver);
        }

        [Test, Description("Add item to wishlist"), Order(0)]
        public void EditGiftCard()
        {
            GiftCardInputInfo info = new GiftCardInputInfo(CheckoutInfo.validFirstName, CheckoutInfo.validLastName, CheckoutInfo.validEmail, CheckoutInfo.validEmail1);
            explorer.OpenPage("gift-cards");
            explorer.GoToItemPage("$25 Virtual Gift Card");
            gift.ChangeRec_Name(info.recName)
                .ChangeRec_Email(info.recEmail)
                .ChangeSend_Name(info.sendName)
                .ChangeSend_Email(info.sendEmail);
            gift.AddItemToCart();
            explorer.OpenCart();
            cart.CheckCardInfo(info);
            cart.StartEditItem("$25 Virtual Gift Card");
            gift.CheckInfo(info.sendName, info.sendEmail, info.recName, info.recEmail);
            info.recName = info.sendName;
            info.recEmail = info.sendEmail;
            info.sendName = CheckoutInfo.validComplexName;
            info.sendEmail = CheckoutInfo.validEmail1;
            gift.ChangeInfo(info.sendName, info.sendEmail, info.recName,info.recEmail);
            gift.AddItemToCart();
            explorer.OpenCart();
            cart.CheckCardInfo(info);
        }
    }
}
