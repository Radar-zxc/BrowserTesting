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
            string validRequest = valid;
            search.AdvancedSearch_TurnOn()
                .NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
        }
        [Test, Description("Check invalid request"), Order(1)]
        public void SimpleInvalid()
        {
            string invalidRequest = invalid;
            search.NewRequest(invalidRequest)
                .UpdateSearch()
                .CheckReceivedItems_Absence(invalidRequest);
        }
        [Test, Description("Check valid request owned only by defined category"), Order(2)]
        public void BelongToCategory()
        {
            string validRequest = valid;
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest);
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category"), Order(3)]
        public void BelongToSubCategory_WithoutAutomaticallySearch()
        {
            string validRequest = validForSubCategory;
            search.NewRequest(validRequest)
                .ChangeCategory("Computers")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .ChangeCategory("Computers >> Desktops")
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category with Automatically search"), 
            Order(4)]
        public void BelongToSubCategory_WithAutomaticallySearch()
        {
            string validRequest = validForSubCategory;
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
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category with Automatically search"),
            Order(5)]
        public void DontBelongToManufacturer()
        {
            string validRequest = valid;
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .ChangeManufacturer("Tricentis")
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .ChangeManufacturer("All");
        }
        [Test, Description("Check item from valid price range"), Order(6)]
        public void ValidPriceRange()
        {
            string validRequest = valid;
            search.NewRequest(validRequest)
                .ChangePrice(0, 1000)
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
        }
        [Test, Description("Check item from invalid price range"), Order(7)]
        public void InvalidPriceRange()
        {
            string invalidRequest = valid;
            search.NewRequest(invalidRequest)
                .ChangePrice(1000, 0)
                .UpdateSearch()
                .CheckReceivedItems_Absence(invalidRequest)
                .ClearPrice();
        }
        [Test, Description("Check request by valid item's description"), Order(8)]
        public void ValidDescription()
        {
            string validRequest = validDescription;
            search.NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems_Absence(validRequest)
                .SearchInDescription_TurnOn()
                .UpdateSearch()
                .CheckReceivedItems(validRequest)
                .SearchInDescription_TurnOff();
        }
        [Test, Description("Check request of 2 characters"), Order(8)]
        public void SmallRequest()
        {
            string invalidRequest = lessThanMinValid;
            search.NewRequest(invalidRequest)
                .UpdateSearch()
                .CheckSmallRequest();
        }
        [Test, Description("Check empty search request from header"), Order(9)]
        public void EmptyRequest_Header()
        {
            string emptyRequest = "";
            search.NewRequest_Header(emptyRequest)
                .UpdateSearch_Header()
                .PopupWindowWithMessageProcessing()
                .AdvancedSearch_TurnOff();
        }
    }
}
