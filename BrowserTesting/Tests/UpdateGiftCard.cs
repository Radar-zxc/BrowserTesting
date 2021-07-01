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
            string itemName = "$25 Virtual Gift Card";
                    test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            GiftCardInputInfo info = new GiftCardInputInfo(CheckoutInfo.validFirstName, CheckoutInfo.validLastName, CheckoutInfo.validEmail, CheckoutInfo.validEmail1);
            explorer.OpenPage("gift-cards");
                    test.Info($"Открытие страницы предмета {itemName}");
            explorer.GoToItemPage(itemName);
                    test.Info($"Заполнение валидной информации в поля Name и Email");
            gift.ChangeRec_Name(info.recName)
                .ChangeRec_Email(info.recEmail)
                .ChangeSend_Name(info.sendName)
                .ChangeSend_Email(info.sendEmail);
                    test.Info($"Добавление предмета {itemName} в корзину");
            gift.AddItemToCart();
                    test.Info($"Переход в корзину");
            explorer.OpenCart();
                    test.Info($"Проверка наличия заполненной ранее информации для предмета {itemName}");
            cart.CheckCardInfo(info);
                    test.Info($"Начало редактирования информации об {itemName}");
            cart.StartEditItem(itemName);
                    test.Info($"Проверка наличия заполненной ранее информации для предмета {itemName}");
            gift.CheckInfo(info.sendName, info.sendEmail, info.recName, info.recEmail);
            info.recName = info.sendName;
            info.recEmail = info.sendEmail;
            info.sendName = CheckoutInfo.validComplexName;
            info.sendEmail = CheckoutInfo.validEmail1;
                    test.Info($"Замена введенной ранее информации в полях для предмета {itemName} на валидные другие");
            gift.ChangeInfo(info.sendName, info.sendEmail, info.recName,info.recEmail);
                    test.Info($"Повторное добавление предмета {itemName} в корзину");
            gift.AddItemToCart();
                    test.Info($"Переход в корзину");
            explorer.OpenCart();
                    test.Info($"Проверка наличия измененной заполненной ранее информации для предмета {itemName}");
            cart.CheckCardInfo(info);
                    test.Pass($"Тест завершен");
        }
    }
}
