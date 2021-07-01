using NUnit.Framework;
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
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("books");
            explorer.UrlVerify("http://demowebshop.tricentis.com/books");
            explorer.ContentVerify("Books");
        }
        [Test, Description("Проверка вкладки Computers"), Order(1)]
        public void CheckComputers()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("computers");
            explorer.UrlVerify("http://demowebshop.tricentis.com/computers");
            explorer.ContentVerify("Computers");
        }
        [Test, Description("Проверка подвкладки Notebooks"), Order(2)]
        public void CheckComputers_Notebooks()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPageWithList("computers", "notebooks");
            explorer.UrlVerify("http://demowebshop.tricentis.com/notebooks");
            explorer.ContentVerify("Notebooks");
        }
        [Test, Description("Проверка подвкладки Desktops"), Order(3)]
        public void CheckComputers_Desktops()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPageWithList("computers", "desktops");
            explorer.UrlVerify("http://demowebshop.tricentis.com/desktops");
            explorer.ContentVerify("Desktops");
        }
        [Test, Description("Проверка вкладки Accessories"), Order(4)]
        public void CheckComputers_Accessories()
        {
            test = extent.CreateTest( DescriptionAttribute.value);
            explorer.OpenPageWithList("computers", "accessories");
            explorer.UrlVerify("http://demowebshop.tricentis.com/accessories");
            explorer.ContentVerify("Accessories");
        }
        [Test, Description("Проверка вкладки Electronics"), Order(5)]
        public void CheckElectronics()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("electronics");
            explorer.UrlVerify("http://demowebshop.tricentis.com/electronics");
            explorer.ContentVerify("Electronics");
        }
        [Test, Description("Проверка подвкладки Camera, photo"), Order(6)]
        public void CheckElectronics_Camera_Photo()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPageWithList("electronics", "camera-photo");
            explorer.UrlVerify("http://demowebshop.tricentis.com/camera-photo");
            explorer.ContentVerify("Camera, photo");
        }
        [Test, Description("Проверка вкладки Cell phones"), Order(7)]
        public void CheckElectronics_CellPhones()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPageWithList("electronics", "cell-phones");
            explorer.UrlVerify("http://demowebshop.tricentis.com/cell-phones");
            explorer.ContentVerify("Cell phones");
        }
        [Test, Description("Проверка вкладки Apparel&Shoes"), Order(8)]
        public void CheckApparelShoes()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("apparel-shoes");
            explorer.UrlVerify("http://demowebshop.tricentis.com/apparel-shoes");
            explorer.ContentVerify("Apparel & Shoes");
        }
        [Test, Description("Проверка вкладки Digital downloads"), Order(9)]
        public void CheckDigitalDownloades()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("digital-downloads");
            explorer.UrlVerify("http://demowebshop.tricentis.com/digital-downloads");
            explorer.ContentVerify("Digital downloads");
        }
        [Test, Description("Проверка вкладки Jewelry"), Order(10)]
        public void CheckJewelry()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("jewelry");
            explorer.UrlVerify("http://demowebshop.tricentis.com/jewelry");
            explorer.ContentVerify("Jewelry");
        }
        [Test, Description("Проверка вкладки Gift Cards"), Order(11)]
        public void CheckGiftCards()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
            explorer.OpenPage("gift-cards");
            explorer.UrlVerify("http://demowebshop.tricentis.com/gift-cards");
            explorer.ContentVerify("Gift Cards");
        }
    }
}
