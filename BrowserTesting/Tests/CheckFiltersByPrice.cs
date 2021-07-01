using NUnit.Framework;
using BrowserTesting.Pages;
namespace BrowserTesting
{
    class CheckFiltersByPrice : TestBase
    {
        private PreOrderPage page;
        private PageExplorer explorer;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            page = new PreOrderPage(Driver);
            explorer = new PageExplorer(Driver);
        }
        [Test, Description("Сhecking the sorting by price on the Jewelry Page")]
        public void CheckFiltersInJewelryPage()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
                    test.Info($"Открытие категории Jewelry");
            explorer.OpenPage("jewelry");
                    test.Info(@$"Проверка фильтров цены ""0-500"" ""700-3000"" ""500-700""");
            page.ChooseFilter("0-500")
                .CheckTable()
                .ChooseFilter("700-3000")
                .CheckTable()
                .ChooseFilter("500-700")
                .CheckTable();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
