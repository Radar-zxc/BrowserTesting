using NUnit.Framework;
using OpenQA.Selenium;
namespace BrowserTesting
{
    [TestFixture]
    public class SearchInGoogle : TestBase
    {
        private readonly By InputSearch = By.XPath("//input[@name='q']");

        [Test, Description("Search with enter"), Order(0)]
        public void SearchWithEnter()
        {
            var enter = Driver.EFindElement(InputSearch);
            enter.Click();
            enter.ESendKeys("ABOBA" + Keys.Enter);
            var check = Driver.EFindElement(By.XPath("//div[@id='rso']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        [Test, Description("Search with button"), Order(1)]
        public void SearchWithButton()
        {
            var enter = Driver.EFindElement(InputSearch);
            enter.Clear();
            string searchTerm = "мышь";
            enter.ESendKeys(searchTerm);
            enter = Driver.EFindElement(By.XPath("//button"));
            enter.Click();
            CheckSearch.Check(searchTerm, Driver);
        }
    }
}
