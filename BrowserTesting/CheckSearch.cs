using NUnit.Framework;
using OpenQA.Selenium;
namespace BrowserTesting
{
    public class CheckSearch: TestBase
    {
        /// <summary>
        /// Вспомогательный метод проверки расположения элемента на странице 
        /// </summary>
        public static void Check(string keys , IWebDriver Driver)
        {
            string xpathCheck = ".//*[text()='" + keys + "']";
            var check = Driver.EFindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
    }
}
