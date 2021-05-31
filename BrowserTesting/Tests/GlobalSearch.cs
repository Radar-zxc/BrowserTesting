using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting.Tests
{
    class GlobalSearch : TestBase
    {
        private SearchPage search;
        private Chain_SearchPage chainSearch;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/search");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            search = new SearchPage(Driver);
            chainSearch = new Chain_SearchPage(Driver);
        }
        [Test, Description("Check valid request"), Order(0)]
        public void SimpleValid()
        {
            string validRequest = InputPreform.preforms["valid"];
            // OLD
            // search.AdvancedSearch_TurnOn();
            // search.NewRequest(validRequest);
            // search.UpdateSearch();
            // search.CheckReceivedItems(validRequest);

            // NEW
            chainSearch.AdvancedSearch_TurnOn()
                .NewRequest(validRequest)
                .UpdateSearch()
                .CheckReceivedItems(validRequest);
        }
        [Test, Description("Check invalid request"), Order(1)]
        public void SimpleInvalid()
        {
            string invalidRequest = InputPreform.preforms["invalid"];
            search.NewRequest(invalidRequest);
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(invalidRequest);
        }
        [Test, Description("Check valid request owned only by defined category"), Order(2)]
        public void BelongToCategory()
        {
            string validRequest = InputPreform.preforms["valid"];
            search.NewRequest(validRequest);
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
            search.ChangeCategory("Computers");
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(validRequest);
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category"), Order(3)]
        public void BelongToSubCategory_WithoutAutomaticallySearch()
        {
            string validRequest = InputPreform.preforms["validForSubCategory"];
            search.NewRequest(validRequest);
            search.ChangeCategory("Computers");
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(validRequest);
            search.ChangeCategory("Computers >> Desktops");
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category with Automatically search"), 
            Order(4)]
        public void BelongToSubCategory_WithAutomaticallySearch()
        {
            string validRequest = InputPreform.preforms["validForSubCategory"];
            search.NewRequest(validRequest);
            search.ChangeCategory("Computers");
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(validRequest);
            search.ChangeCategory("Computers >> Desktops");
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
            search.AutomaticallySearchSubCategories_TurnOn();
            search.ChangeCategory("Computers");
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
            search.AutomaticallySearchSubCategories_TurnOff();
            search.ChangeCategory("All");
        }
        [Test, Description("Check item in valid request which owned only by defined sub-category with Automatically search"),
            Order(5)]
        public void DontBelongToManufacturer()
        {
            string validRequest = InputPreform.preforms["valid"];
            search.NewRequest(validRequest);
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
            search.ChangeManufacturer("Tricentis");
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(validRequest);
            search.ChangeManufacturer("All");
        }
        [Test, Description("Check item from valid price range"), Order(6)]
        public void ValidPriceRange()
        {
            string validRequest = InputPreform.preforms["valid"];
            search.NewRequest(validRequest);
            search.ChangePrice(0, 1000);
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
        }
        [Test, Description("Check item from invalid price range"), Order(7)]
        public void InvalidPriceRange()
        {
            string invalidRequest = InputPreform.preforms["valid"];
            search.NewRequest(invalidRequest);
            search.ChangePrice(1000, 0);
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(invalidRequest);
            search.ClearPrice();
        }
        [Test, Description("Check request by valid item's description"), Order(8)]
        public void ValidDescription()
        {
            string validRequest = InputPreform.preforms["validDescription"];
            search.NewRequest(validRequest);
            search.UpdateSearch();
            search.CheckReceivedItems_Absence(validRequest);
            search.SearchInDescription_TurnOn();
            search.UpdateSearch();
            search.CheckReceivedItems(validRequest);
            search.SearchInDescription_TurnOff();
        }
        [Test, Description("Check request of 2 characters"), Order(8)]
        public void SmallRequest()
        {
            string invalidRequest = InputPreform.preforms["lessThanMinValid"];
            search.NewRequest(invalidRequest);
            search.UpdateSearch();
            search.CheckSmallRequest();
        }
        [Test, Description("Check empty search request from header"), Order(9)]
        public void EmptyRequest_Header()
        {
            string emptyRequest = "";
            search.NewRequest_Header(emptyRequest);
            search.UpdateSearch_Header();
            search.PopupWindowWithMessageProcessing();
        }
    }
}
