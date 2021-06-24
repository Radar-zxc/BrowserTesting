using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, предназначенный для обработки страницы Gift Card
    /// </summary>
    class GiftCardPage : OrderPage
    {
        public GiftCardPage(IWebDriver Driver) : base(Driver)
        {

        }
        /// <summary>
        /// Метод изменения значения в поле Recipient's name
        /// </summary>
        public GiftCardPage ChangeRec_Name(string name)
        {
            IWebElement nameField = Driver.EFindElement(By.ClassName("recipient-name"));
            nameField.Clear();
            nameField.ESendKeys(name);
            return this;
        }
        /// <summary>
        /// Метод изменения значения в поле Recipient's email
        /// </summary>
        public GiftCardPage ChangeRec_Email(string email)
        {
            IWebElement emailField = Driver.EFindElement(By.ClassName("recipient-email"));
            emailField.Clear();
            emailField.ESendKeys(email);
            return this;
        }
        /// <summary>
        /// Метод изменения значения в поле Your Name
        /// </summary>
        public GiftCardPage ChangeSend_Name(string name)
        {
            IWebElement nameField = Driver.EFindElement(By.ClassName("sender-name"));
            nameField.Clear();
            nameField.ESendKeys(name);
            return this;
        }
        /// <summary>
        /// Метод изменения значения в поле Your Email
        /// </summary>
        public GiftCardPage ChangeSend_Email(string email)
        {
            IWebElement emailField = Driver.EFindElement(By.ClassName("sender-email"));
            emailField.Clear();
            emailField.ESendKeys(email);
            return this;
        }
        /// <summary>
        /// Метод изменения значения в поле Message
        /// </summary>
        public GiftCardPage ChangeMessage(string text)
        {
            IWebElement emailField = Driver.EFindElement(By.ClassName("message"));
            emailField.Clear();
            emailField.ESendKeys(text);
            return this;
        }
        /// <summary>
        /// Метод изменения значений всех строковых полей для предмета Gift Card на странице заказа 
        /// </summary>
        public GiftCardPage ChangeInfo(string sendName, string sendEmail , string recName,string recEmail)
        {
            ChangeSend_Name(sendName).ChangeSend_Email(sendEmail).ChangeRec_Name(recName).ChangeRec_Email(recEmail);
            return this;
        }
        /// <summary>
        /// Метод проверки значений в строковых полях после начала редактирования на странице Cart
        /// </summary>
        public GiftCardPage CheckInfo(string sendName, string sendEmail, string recName, string recEmail)
        {
            Assert.IsTrue(Driver.FindElements(By.XPath($"//input[@value='{sendName}']")).Count != 0, "Your Name отличается от исходного");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//input[@value='{sendEmail}']")).Count != 0, "Your Email отличается от исходного");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//input[@value='{recName}']")).Count != 0, "Recipient's Name отличается от исходного");
            Assert.IsTrue(Driver.FindElements(By.XPath($"//input[@value='{recEmail}']")).Count != 0, "Recipient's Email отличается от исходного");
            return this;
        }
    }
}
