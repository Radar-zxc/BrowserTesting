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
        /// <summary>
        /// Метод начинающий оформление заказа в качестве гостя
        /// </summary>
        public CheckoutPage CheckoutAsGuest()
        {
            ClickOnElement(By.CssSelector(".checkout-as-guest-button"));
            return this;
        }
        /// <summary>
        /// Метод заполнения ключевых полей на шаге Billing
        /// </summary>
        public CheckoutPage WriteBillingInfo()
        {
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
            ClickOnElement(By.CssSelector("input#PickUpInStore"));
            ClickOnElement(By.XPath("//input[contains(@onclick,'Shipping')][@class='button-1 new-address-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод выбора способа оплаты на шаге Payment Method
        /// </summary>
        public CheckoutPage ChoosePayment(string method)
        {
            ClickOnElement(By.XPath($"//label[contains(text(),'{method}')]/..//input"));
            ClickOnElement(By.XPath("//input[contains(@onclick,'Payment')][@class='button-1 payment-method-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод подтверждения информации о способе оплаты
        /// </summary>
        public CheckoutPage PaymentInfonmation()
        {
            ClickOnElement(By.XPath("//input[contains(@onclick,'Payment')][@class='button-1 payment-info-next-step-button']"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод завершения оформления заказа
        /// </summary>
        public CheckoutPage ConfirmOrder()
        {
            ClickOnElement(By.CssSelector(".confirm-order-next-step-button"));
            WaitLoading();
            return this;
        }
        /// <summary>
        /// Метод проверки завершения оформления заказа
        /// </summary>
        public CheckoutPage CheckConfirm()
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
    }
}
