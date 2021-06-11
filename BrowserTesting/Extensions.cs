using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace BrowserTesting
{
    static class Extensions
    {
        /// <summary>
        /// Метод, сокращающий цепочку методов для выполения передвижения к элементу 
        /// </summary>
        public static IWebElement MoveToElement (this IWebDriver Driver,IWebElement elem )
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(elem).Build().Perform();
            return elem;
        }
        /// <summary>
        /// Метод, добавляющий в FindElement внутреннюю обработку исключений 
        /// </summary>
        public  static IWebElement EFindElement(this IWebDriver Driver, By elem)
        {
            IWebElement result = null;
            try
            {
                result = Driver.FindElement(elem);
            }
            catch (NoSuchElementException e)
            {
                throw new Exception($"Искомый элемент отсутствует на странице \n {e.Message}");

            }
            return result;
        }
        /// <summary>
        /// Метод, добавляющий в Click внутреннюю обработку исключений 
        /// </summary>
        public static void EClick(this IWebDriver Driver, IWebElement elem)
        {
            try
            {
                elem.Click();
            }
            catch(WebDriverException)
            {
                try
                {
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
                    executor.ExecuteScript("arguments[0].click();", elem);
                }
                catch (WebDriverException e)
                {
                    if (e is ElementNotVisibleException)
                    {
                        throw new Exception($"Попытка клика по невидимому элементу \n {e.Message}");
                    }
                    else if (e is StaleElementReferenceException)
                    {
                        throw new Exception($"Ссылка на элемент более недействительна \n {e.Message}");
                    }
                }
            }
        }
        /// <summary>
        /// Метод, добавляющий в SendKeys внутреннюю обработку исключений 
        /// </summary>
        public static void ESendKeys(this IWebElement elem, string text)
        {
            try
            {
                elem.SendKeys(text);
            }
            catch (InvalidElementStateException e)
            {
                throw new Exception($"Элемент находится в недопустимом к взаимодействию состоянии" +
                    $"(перекрыт другим элементом, элемент невидим)  \n {e.Message}");
            }
            catch(StaleElementReferenceException e)
            {
                throw new Exception($"Ссылка на элемент более недействительна \n {e.Message}");
            }
        }
    }
}
