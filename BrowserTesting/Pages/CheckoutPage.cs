using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, предназначенный для обработки страницы подтверждения заказа
    /// </summary>
    class CheckoutPage : BasePage 
    {
        public CheckoutPage(IWebDriver Driver) : base(Driver)
        {

        }
        private By firstName = By.Id("BillingNewAddress_FirstName");
        private By lastName = By.Id("BillingNewAddress_LastName");
        private By email = By.Id("BillingNewAddress_Email");
        private By country = By.Id("BillingNewAddress_CountryId");
        private By city = By.Id("BillingNewAddress_City");
        private By address1 = By.Id("BillingNewAddress_Address1");
        private By zipPostalCode = By.Id("BillingNewAddress_ZipPostalCode");
        private By phoneNumber = By.Id("BillingNewAddress_PhoneNumber");
        private double additionalPaymentCost = 0;
        /// <summary>
        /// Метод начинающий оформление заказа в качестве гостя
        /// </summary>
        public CheckoutPage CheckoutAsGuest()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h1[contains(text(),'Sign')]")).Displayed
                , "Страница входа в систему не открыта");
            ClickOnElement(By.CssSelector(".checkout-as-guest-button"));
            return this;
        }
        /// <summary>
        /// Метод заполнения ключевых полей на шаге Billing
        /// </summary>
        public CheckoutPage WriteBillingInfo()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Billing address']//../../div[@class='step a-item']")).Displayed
                , "Шаг Billing address не открыт");
            Driver.EFindElement(firstName).ESendKeys(CheckoutInfo.validFirstName);
            Driver.EFindElement(lastName).ESendKeys(CheckoutInfo.validLastName);
            Driver.EFindElement(email).ESendKeys(CheckoutInfo.validEmail);
            SelectElement countryName = new SelectElement(Driver.EFindElement(country));
            countryName.SelectByText(CheckoutInfo.country);
            Driver.EFindElement(city).ESendKeys(CheckoutInfo.validCity);
            Driver.EFindElement(address1).ESendKeys(CheckoutInfo.validAddress1);
            Driver.EFindElement(zipPostalCode).ESendKeys(CheckoutInfo.validZip);
            Driver.EFindElement(phoneNumber).ESendKeys(CheckoutInfo.validPhoneNumber);
            ClickOnElement(By.XPath("//input[contains(@onclick,'Billing')]"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод для обработки шага Shipping Address
        /// </summary>
        public CheckoutPage WriteShippingInfo()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Shipping address']//../../div[@class='step a-item']")).Displayed
                , "Шаг Shipping address не открыт");

            if (CheckoutInfo.inStorePickUp)
            {
                ClickOnElement(By.CssSelector("input#PickUpInStore"));
            }
            ClickOnElement(By.XPath("//input[contains(@onclick,'Shipping')][@class='button-1 new-address-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод выбора способа доставки на шаге Shipping Method
        /// </summary>
        public CheckoutPage ChooseShipping()
        {
            if (!CheckoutInfo.inStorePickUp)
            {
                Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Shipping method']//../../div[@class='step a-item']")).Displayed
                    , "Шаг Shipping Method не открыт");
                ClickOnElement(By.XPath($"//div[@class='method-name']//input[contains(@value,'{CheckoutInfo.shippingMethod}')]"));
                ClickOnElement(By.XPath("//input[contains(@onclick,'ShippingMethod')][@class='button-1 shipping-method-next-step-button']"));
                WaitLoading();
            }
            return this;
        }
        /// <summary>
        /// Метод выбора способа оплаты на шаге Payment Method
        /// </summary>
        public CheckoutPage ChoosePayment()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Payment method']//../../div[@class='step a-item']")).Displayed
                , "Шаг Payment Method не открыт");
            ClickOnElement(By.XPath($"//label[contains(text(),'{CheckoutInfo.paymentMethod}')]/..//input"));
            GetPaymentAdditionalFee();
            ClickOnElement(By.XPath("//input[contains(@onclick,'Payment')][@class='button-1 payment-method-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод получения числового значения для Payment Method
        /// </summary>
        private void GetPaymentAdditionalFee()
        {
            string innerText = Driver.EFindElement(By.XPath($"//label[contains(text(),'{CheckoutInfo.paymentMethod}')]")).Text;
            if (innerText[innerText.LastIndexOf(' ')+1] == '(')
            {
                int beginCost = innerText.LastIndexOf(' ') + 2;
                int endCost = innerText.LastIndexOf(')');
                additionalPaymentCost = Convert.ToDouble(
                    innerText.Substring(beginCost, endCost- beginCost)
                    .Replace('.', ','));
            }
        }
        /// <summary>
        /// Метод подтверждения информации о способе оплаты
        /// </summary>
        public CheckoutPage PaymentInfonmation()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Payment information']//../../div[@class='step a-item']")).Displayed
                , "Шаг Payment information не открыт");
            ClickOnElement(By.XPath("//input[contains(@onclick,'Payment')][@class='button-1 payment-info-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод завершения оформления заказа
        /// </summary>
        public CheckoutPage ConfirmOrder()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//h2[text()='Confirm order']//../../div[@class='step a-item']")).Displayed
                , "Шаг Confirm order не открыт");

            BillingVerify();
            ShippingVerify();
            TotalCostVerify();
            ClickOnElement(By.CssSelector(".confirm-order-next-step-button"));
            WaitLoading();
            CheckConfirm();
            return this;
        }
        /// <summary>
        /// Метод проверки завершения оформления заказа
        /// </summary>
        private CheckoutPage CheckConfirm()
        {
            Assert.IsTrue(Driver.EFindElement(By.XPath("//li[contains(text(),'Order number')]")).Displayed, "Заказ не оформлен");
            return this;
        }
        /// <summary>
        /// Метод ожидания обработки шага оформления
        /// </summary>
        private void WaitLoading()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@disabled]")));
        }
        /// <summary>
        /// Метод проверки рассчета итоговой стоимости заказа
        /// </summary>
        private void TotalCostVerify()
        {
            double subTotal = Convert.ToDouble(
                Driver.EFindElement(By.XPath("//span[contains(text(),'Sub-Total')]/../..//td[@class='cart-total-right']//span[@class='product-price']"))
                .Text.Replace('.', ','));
            double shipping = Convert.ToDouble(
                Driver.EFindElement(By.XPath("//span[contains(text(),'Shipping')]/../..//td[@class='cart-total-right']//span[@class='product-price']"))
                .Text.Replace('.', ','));
            double tax = Convert.ToDouble(
                Driver.EFindElement(By.XPath("//span[contains(text(),'Tax')]/../..//td[@class='cart-total-right']//span[@class='product-price']"))
                .Text.Replace('.', ','));
            double total = Convert.ToDouble(
                Driver.EFindElement(By.CssSelector(".product-price.order-total strong"))
                .Text.Replace('.', ','));
            Assert.AreEqual(subTotal+shipping+tax+additionalPaymentCost, total, "Рассчет итоговой цены заказа произошел неверно");
        }
        /// <summary>
        /// Метод верификации введенных параметров для Billing Address
        /// </summary>
        private void BillingVerify()
        {
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='name']" +
                $"[contains(text(),'{CheckoutInfo.validFirstName + ' ' + CheckoutInfo.validLastName }')]")).Count!=0
                , "Несоотвествие вводимого и итогового First name, Last name");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='email']" +
                $"[contains(text(),'{CheckoutInfo.validEmail}')]")).Count != 0
                , "Несоотвествие вводимого и итогового Email");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='phone']" +
                $"[contains(text(),'{CheckoutInfo.validPhoneNumber}')]")).Count != 0
                , "Несоотвествие вводимого и итогового Phone number");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='address1']" +
                $"[contains(text(),'{CheckoutInfo.validAddress1}')]")).Count != 0
                , "Несоотвествие вводимого и итогового Address 1");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='city-state-zip']" +
                $"[contains(normalize-space(text()),'{CheckoutInfo.validCity + " , " + CheckoutInfo.state + ' ' + CheckoutInfo.validZip}')]")).Count != 0
                , "Несоотвествие вводимого и итогового City, State, Zip / postal code");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='country']" +
                $"[contains(text(),'{CheckoutInfo.country}')]")).Count != 0
                , "Несоотвествие вводимого и итогового Country");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='billing-info']//li[@class='payment-method']" +
                $"[contains(text(),'{CheckoutInfo.paymentMethod}')]")).Count!=0
                , "Несоотвествие выбранного и итогового Payment Method");
        }
        /// <summary>
        /// Метод верификации введенных параметров для Shipping Address и Shipping Method
        /// </summary>
        private void ShippingVerify()
        {
            if (!CheckoutInfo.inStorePickUp)
            {
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='name']" +
                     $"[contains(text(),'{CheckoutInfo.validFirstName + ' ' + CheckoutInfo.validLastName }')]")).Count != 0
                    , "Несоотвествие вводимого и итогового First name, Last name");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='email']" +
                    $"[contains(text(),'{CheckoutInfo.validEmail}')]")).Count != 0
                    , "Несоотвествие вводимого и итогового Email");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='phone']" +
                    $"[contains(text(),'{CheckoutInfo.validPhoneNumber}')]")).Count != 0
                    , "Несоотвествие вводимого и итогового Phone number");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='address1']" +
                    $"[contains(text(),'{CheckoutInfo.validAddress1}')]")).Count != 0
                    , "Несоотвествие вводимого и итогового Address 1");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='city-state-zip']" +
                    $"[contains(normalize-space(text()),'{CheckoutInfo.validCity + " , " + CheckoutInfo.state + ' ' + CheckoutInfo.validZip}')]")).Count != 0
                    , "Несоотвествие вводимого и итогового City, State, Zip / postal code");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='country']" +
                    $"[contains(text(),'{CheckoutInfo.country}')]")).Count != 0
                    , "Несоотвествие вводимого и итогового Country");
                Assert.IsTrue(Driver.FindElements(By.XPath($"//ul[@class='shipping-info']//li[@class='shipping-method']" +
                    $"[contains(text(),'{CheckoutInfo.shippingMethod}')]")).Count != 0
                    , "Несоотвествие выбранного и итогового Shipping Method");
            }
            else
            {
                Assert.IsTrue(Driver.FindElements(By.XPath("//li[contains(text(),'In-Store Pickup')]")).Count!=0
                    , "Несоответствие итогового состояния параметра In-Store Pickup ");
            }
        }
    }
}
