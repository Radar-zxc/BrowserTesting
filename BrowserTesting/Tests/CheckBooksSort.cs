using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class CheckBooksSort : TestBase
    {
        private PageExplorer explorer;
        private PreOrderPage page;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            page = new PreOrderPage(Driver);
        }
        [Test, Description("Check Sorts on books page"), Order(0)]
        public void CheckSort()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
                    test.Info($"Открытие категории Books");
            explorer.OpenPage("books");
                    test.Info($"Проверка сортировок A to Z, Z to A, Low to High, High to Low");
            page.CheckSort();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
