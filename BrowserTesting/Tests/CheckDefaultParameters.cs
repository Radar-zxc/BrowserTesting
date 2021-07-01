using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{

    class CheckDefaultParameters : TestBase
    {
        private PageExplorer explorer;
        private ComputerPage computer;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
            computer = new ComputerPage(Driver);
        }
        [Test, Description("Check default parameters on 'Build your own computer' page"), Order(0)]
        public void CheckParameters()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
                    test.Info($"Открытие категории Computers -> Desktops");
            explorer.OpenPageWithList("computers", "desktops");
                    test.Info(@$"Открытие страницы ""Build your own computer"" ");
            explorer.GoToItemPage("Build your own computer");
                    test.Info($"Проверка установленных параметров по умолчанию согласно требованиям ");
            computer.StartCheckDefaultParameters();
            explorer.OpenStartPage();
                    test.Pass($"Тест завершен");
        }
    }
}
