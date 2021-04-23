using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace BrowserTesting
{
    class PageUrlChecking:TestBase
    {
        override public void DriverSetUp()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
        }
        [Test,Description("Проверка вкладки Books"),Order(0)]
        public void CheckBooks()
        {
            OpenPage("books");
            UrlVerify("http://demowebshop.tricentis.com/books");
            ContentVerify("Books");
        }
        [Test, Description("Проверка вкладки Computers"), Order(1)]
        public void CheckComputers()
        {
            OpenPage("computers");
            UrlVerify("http://demowebshop.tricentis.com/computers");
            ContentVerify("Computers");
        }
        [Test, Description("Проверка подвкладки Notebooks"), Order(2)]
        public void CheckComputers_Notebooks()
        {
            OpenPageWithList("computers", "notebooks");
            UrlVerify("http://demowebshop.tricentis.com/notebooks");
            ContentVerify("Notebooks");
        }
        [Test, Description("Проверка подвкладки Desktops"), Order(3)]
        public void CheckComputers_Desktops()
        {
            OpenPageWithList("computers", "desktops");
            UrlVerify("http://demowebshop.tricentis.com/desktops");
            ContentVerify("Desktops");
        }
        [Test, Description("Проверка вкладки Accessories"), Order(4)]
        public void CheckComputers_Accessories()
        {
            OpenPageWithList("computers", "accessories");
            UrlVerify("http://demowebshop.tricentis.com/accessories");
            ContentVerify("Accessories");
        }
        [Test, Description("Проверка вкладки Electronics"), Order(5)]
        public void CheckElectronics()
        {
            OpenPage("electronics");
            UrlVerify("http://demowebshop.tricentis.com/electronics");
            ContentVerify("Electronics");
        }
        [Test, Description("Проверка подвкладки Camera, photo"), Order(6)]
        public void CheckElectronics_Camera_Photo()
        {
            OpenPageWithList("electronics", "camera-photo");
            UrlVerify("http://demowebshop.tricentis.com/camera-photo");
            ContentVerify("Camera, photo");
        }
        [Test ,Description("Проверка вкладки Cell phones"), Order(7)]
        public void CheckElectronics_CellPhones()
        {
            OpenPageWithList("electronics", "cell-phones");
            UrlVerify("http://demowebshop.tricentis.com/cell-phones");
            ContentVerify("Cell phones");
        }
        [Test, Description("Проверка вкладки Apparel&Shoes"), Order(8)]
        public void CheckApparelShoes()
        {
            OpenPage("apparel-shoes");
            UrlVerify("http://demowebshop.tricentis.com/apparel-shoes");
            ContentVerify("Apparel & Shoes");
        }
        [Test ,Description("Проверка вкладки Digital downloads"), Order(9)]
        public void CheckDigitalDownloades()
        {
            OpenPage("digital-downloads");
            UrlVerify("http://demowebshop.tricentis.com/digital-downloads");
            ContentVerify("Digital downloads");
        }
        [Test ,Description("Проверка вкладки Jewelry"), Order(10)]
        public void CheckJewelry()
        {
            OpenPage("jewelry");
            UrlVerify("http://demowebshop.tricentis.com/jewelry");
            ContentVerify("Jewelry");
        }
        [Test ,Description("Проверка вкладки Gift Cards"), Order(11)]
        public void CheckGiftCards()
        {
            OpenPage("gift-cards");
            UrlVerify("http://demowebshop.tricentis.com/gift-cards");
            ContentVerify("Gift Cards");
        }
    }
}
