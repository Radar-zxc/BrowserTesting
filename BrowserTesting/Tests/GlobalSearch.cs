using NUnit.Framework;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class GlobalSearch : TestBase
    {
        private SearchPage search;
        private static string valid = "Science";
        private static string lessThanMinValid = "bo";
        private static string invalid = "zp9";
        private static string validForSubCategory = "desktop";
        private static string validDescription = "must";
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/search");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            search = new SearchPage(Driver);
        }
        [Test, Description("Check valid request"), Order(0)]
        public void SimpleValid()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
            string validRequest = valid;
                    test.Info($"Начало проверки работы поиска с валидным запросом ");
            search.AdvancedSearch_TurnOn()
                .NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check invalid request"), Order(1)]
        public void SimpleInvalid()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string invalidRequest = invalid;
                    test.Info($"Начало проверки работы поиска с невалидным запросом ");
            search.NewRequest(invalidRequest)
                .UpdateSearch()
                .CheckReceivedItems_Absence(invalidRequest);
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check valid request owned only by defined category"), Order(2)]
        public void BelongToCategory()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = valid;
                    test.Info($"Начало проверки работы поиска только в определенной категории");
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest);
                    test.Pass("Тест завершен");
        }
        [Test, Description("Check item valid request which owned only by defined sub-category"), Order(3)]
        public void BelongToSubCategory_WithoutAutomaticallySearch()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = validForSubCategory;
                    test.Info($"Начало проверки работы поиска только в определенной подкатегории");
            search.NewRequest(validRequest)
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .ChangeCategory("Computers >> Desktops")
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check item valid request which owned only by defined sub-category with Automatically search"), 
            Order(4)]
        public void BelongToSubCategory_WithAutomaticallySearch()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = validForSubCategory;
                    test.Info($"Начало проверки работы поиска по определенной категории с учетом ее подкатегорий (Automatically search sub categories)");
            search.NewRequest(validRequest)
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .ChangeCategory("Computers >> Desktops")
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .AutomaticallySearchSubCategories_TurnOn()
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .AutomaticallySearchSubCategories_TurnOff()
                .ChangeCategory("All");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check item valid request which belong defined manufacturer"),
            Order(5)]
        public void DontBelongToManufacturer()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = valid;
                    test.Info($"Начало проверки работы поиска с учетом определенного Manufacturer");
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .ChangeManufacturer("Tricentis")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .ChangeManufacturer("All");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check item from valid price range"), Order(6)]
        public void ValidPriceRange()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = valid;
                    test.Info($"Начало проверки функции поиска по определенному валидному ценовому диапазону");
            search.NewRequest(validRequest)
                .ChangePrice(0, 1000)
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check item from invalid price range"), Order(7)]
        public void InvalidPriceRange()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string invalidRequest = valid;
                    test.Info($"Начало проверки функции поиска по определенному невалидному ценовому диапазону");
            search.NewRequest(invalidRequest)
                .ChangePrice(1000, 0)
                .UpdateSearch()
                .CheckReceivedItems_Absence(invalidRequest)
                .ClearPrice();
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check request by valid item's description"), Order(8)]
        public void ValidDescription()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string validRequest = validDescription;
                    test.Info($"Начало проверки функции поиска по запросу среди описаний предметов (Search In product descriptions)");
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .SearchInDescription_TurnOn()
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .SearchInDescription_TurnOff();
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check request of 2 characters"), Order(8)]
        public void SmallRequest()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string invalidRequest = lessThanMinValid;
                    test.Info($"Проверка обработки невалидного запроса, состоящего из 2 символов");
            search.NewRequest(invalidRequest)
                .UpdateSearch()
                .CheckSmallRequest();
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Check empty search request from header"), Order(9)]
        public void EmptyRequest_Header()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            string emptyRequest = "";
                    test.Info($"Проверка обработки пустого запроса, введенного в строку поиска вверху страницы");
            search.NewRequest_Header(emptyRequest)
                .UpdateSearch_Header()
                .PopupWindowWithMessageProcessing()
                .AdvancedSearch_TurnOff();
                    test.Pass($"Тест завершен");
        }
    }
}
