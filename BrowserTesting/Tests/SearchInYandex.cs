using NUnit.Framework;
using OpenQA.Selenium;
namespace BrowserTesting
{
    [TestFixture]
    public class SearchInYandex : TestBase
    {
        private readonly By InputSearch = By.Id("text");

        [Test,Description("Search with enter"),Order(0)]
        public void SearchWithEnter()
        {
            var enter = Driver.EFindElement(InputSearch);
            enter.Click();
            enter.ESendKeys("ABOBA");
            enter.ESendKeys(Keys.Enter);
            var check = Driver.EFindElement(By.XPath("//ul[@id='search-result']//*[text()='ABOBA']"));
            Assert.IsTrue(check.Displayed, "Искомая информация не найдена");
        }
        [Test , Description("Search with button"),Order(1)]
        public void SearchWithButton()
        {
            var enter = Driver.EFindElement(By.XPath("//input[@name='text']"));
            enter.Clear();
            string searchTerm = "Audi";
            enter.ESendKeys(searchTerm);
            enter = Driver.EFindElement(By.ClassName("websearch-button__text"));
            enter.Click();
            CheckSearch.Check(searchTerm, Driver);
        }
    }
}
