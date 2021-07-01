using NUnit.Framework;
using OpenQA.Selenium;
namespace BrowserTesting
{
    public class CheckSearch: TestBase
    {
        public static void Check(string keys , IWebDriver Driver)
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string xpathCheck = ".//*[text()='" + keys + "']";
            var check = Driver.EFindElement(By.XPath(xpathCheck));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
    }
}
