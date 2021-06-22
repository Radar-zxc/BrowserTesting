using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий общие методы для других классов страниц
    /// </summary>
    abstract class BasePage 
    {
        protected IWebDriver Driver ;
        public BasePage(IWebDriver Driver )
        {
            this.Driver = Driver;
        }
        /// <summary>
        /// Метод изменения количества предметов по заданному локатору поля на заданное новое количество
        /// </summary>
        protected void ChangeCount(By countField, int newCount)
        {
            var action = Driver.EFindElement(countField);
            action.Clear();
            action.ESendKeys(newCount.ToString());
        }
        /// <summary>
        /// Метод нажатия на элемент по заданному локатору
        /// </summary>
        protected void ClickOnElement(By elem)
        {
            var action = Driver.EFindElement(elem);
            Driver.EClick(action);
            //для IE Driver.MoveToElement(action).Click();
        }
        /// <summary>
        /// Метод выбора определенного параметра в выпадающем списке по соответствующим локаторам 
        /// </summary>
        protected void PickParameterInPopupList(By popupList, By popupParameter)
        {
            var list = Driver.EFindElement(popupList);
            Driver.MoveToElement(list).Click();
            ClickOnElement(popupParameter);
        }
        /// <summary>
        /// Метод открытия страницы с заданным локатором в новой вкладке
        /// </summary>
        protected void OpenPageRef(By page)
        {
            Driver.EFindElement(page).ESendKeys(Keys.Control + Keys.Shift + Keys.Enter);
        }
        /// <summary>
        /// Переключение CheckBox в состояние Выбрано
        /// </summary>
        public void CheckBox_TurnOn(By checkBox)
        {
            IWebElement elem = Driver.EFindElement(checkBox);
            if (!elem.Selected)
            {
                Driver.EClick(elem);
            }
        }
        /// <summary>
        /// Переключение CheckBox в состояние Не выбрано
        /// </summary>
        public void CheckBox_TurnOff(By checkBox)
        {
            IWebElement elem = Driver.EFindElement(checkBox);
            if (elem.Selected)
            {
                Driver.EClick(elem);
            }
        }
        /// <summary>
        /// Метод ожидания скрытия колеса загрузки на странице
        /// </summary>
        protected void WaitLoadingCircle()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".loading-image")));
        }
    }
}
