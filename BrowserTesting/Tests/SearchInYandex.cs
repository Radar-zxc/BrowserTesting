using NUnit.Framework;
using OpenQA.Selenium;
namespace BrowserTesting
{
    [TestFixture]
    public class SearchInYandex : TestBase
    {
        private readonly By InputSearch = By.Id("text");

        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("https://yandex.ru");
        }
        [Test,Description("Search with enter"),Order(0)]
        public void SearchWithEnter()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница поисковика");
            var enter = Driver.EFindElement(InputSearch);
                    test.Info($"Клик на строку для ввода ");
            enter.Click();
                    test.Info($"Ввод поискового запроса в соответствующую строку и нажатие клавиши Enter");
            enter.ESendKeys("ABOBA");
            enter.ESendKeys(Keys.Enter);
                    test.Info($"Проверка наличия информации на странице в соответствии с произведенным запросом ");
            var check = Driver.EFindElement(By.XPath("//ul[@id='search-result']//*[text()='ABOBA']"));
                    test.Pass($"Тест завершен");
        }
        [Test , Description("Search with button"),Order(1)]
        public void SearchWithButton()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Очистка поля для ввода запроса");
            var enter = Driver.EFindElement(By.XPath("//input[@name='text']"));
            enter.Clear();
            string searchTerm = "Audi";
                    test.Info($"Ввод нового текста запроса в соответствующее поле");
            enter.ESendKeys(searchTerm);
            enter = Driver.EFindElement(By.ClassName("websearch-button__text"));
                    test.Info($"Нажатие на кнопку поиска на странице по соответствующему локатору");
            enter.Click();
                    test.Info($"Проверка наличия информации на странице в соответствии с произведенным запросом ");
            string xpathCheck = ".//*[text()='" + searchTerm + "']";
            Driver.EFindElement(By.XPath(xpathCheck));
                    test.Pass($"Тест завершен");
        }
    }
}
