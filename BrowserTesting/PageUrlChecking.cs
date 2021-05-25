using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using BrowserTesting.Pages;

namespace BrowserTesting
{
    class PageUrlChecking : TestBase
    {
        private PageExplorer explorer;
        override public void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            explorer = new PageExplorer(Driver);
        }
        [Test, Description("Проверка вкладки Books"),Order(0)]
        public void CheckBooks()
        {
            explorer.OpenPage("books");
            UrlVerify("http://demowebshop.tricentis.com/books");
            ContentVerify("Books");
        }
        [Test, Description("Проверка вкладки Computers"), Order(1)]
        public void CheckComputers()
        {
            explorer.OpenPage("computers");
            UrlVerify("http://demowebshop.tricentis.com/computers");
            ContentVerify("Computers");
        }
        [Test, Description("Проверка подвкладки Notebooks"), Order(2)]
        public void CheckComputers_Notebooks()
        {
            explorer.OpenPageWithList("computers", "notebooks");
            UrlVerify("http://demowebshop.tricentis.com/notebooks");
            ContentVerify("Notebooks");
        }
        [Test, Description("Проверка подвкладки Desktops"), Order(3)]
        public void CheckComputers_Desktops()
        {
            explorer.OpenPageWithList("computers", "desktops");
            UrlVerify("http://demowebshop.tricentis.com/desktops");
            ContentVerify("Desktops");
        }
        [Test, Description("Проверка вкладки Accessories"), Order(4)]
        public void CheckComputers_Accessories()
        {
            explorer.OpenPageWithList("computers", "accessories");
            UrlVerify("http://demowebshop.tricentis.com/accessories");
            ContentVerify("Accessories");
        }
        [Test, Description("Проверка вкладки Electronics"), Order(5)]
        public void CheckElectronics()
        {
            explorer.OpenPage("electronics");
            UrlVerify("http://demowebshop.tricentis.com/electronics");
            ContentVerify("Electronics");
        }
        [Test, Description("Проверка подвкладки Camera, photo"), Order(6)]
        public void CheckElectronics_Camera_Photo()
        {
            explorer.OpenPageWithList("electronics", "camera-photo");
            UrlVerify("http://demowebshop.tricentis.com/camera-photo");
            ContentVerify("Camera, photo");
        }
        [Test, Description("Проверка вкладки Cell phones"), Order(7)]
        public void CheckElectronics_CellPhones()
        {
            explorer.OpenPageWithList("electronics", "cell-phones");
            UrlVerify("http://demowebshop.tricentis.com/cell-phones");
            ContentVerify("Cell phones");
        }
        [Test, Description("Проверка вкладки Apparel&Shoes"), Order(8)]
        public void CheckApparelShoes()
        {
            explorer.OpenPage("apparel-shoes");
            UrlVerify("http://demowebshop.tricentis.com/apparel-shoes");
            ContentVerify("Apparel & Shoes");
        }
        [Test, Description("Проверка вкладки Digital downloads"), Order(9)]
        public void CheckDigitalDownloades()
        {
            explorer.OpenPage("digital-downloads");
            UrlVerify("http://demowebshop.tricentis.com/digital-downloads");
            ContentVerify("Digital downloads");
        }
        [Test, Description("Проверка вкладки Jewelry"), Order(10)]
        public void CheckJewelry()
        {
            explorer.OpenPage("jewelry");
            UrlVerify("http://demowebshop.tricentis.com/jewelry");
            ContentVerify("Jewelry");
        }
        [Test, Description("Проверка вкладки Gift Cards"), Order(11)]
        public void CheckGiftCards()
        {
            explorer.OpenPage("gift-cards");
            UrlVerify("http://demowebshop.tricentis.com/gift-cards");
            ContentVerify("Gift Cards");
        }
    }
}
