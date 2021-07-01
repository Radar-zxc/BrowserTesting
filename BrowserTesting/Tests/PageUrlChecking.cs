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
                    test.Info($"Открыта базовая страница");
                    test.Info($"Переход в категорию Books");
            explorer.OpenPage("books");
                    test.Info($"Проверка перехода в категорию Books");
            explorer.UrlVerify("http://demowebshop.tricentis.com/books");
            explorer.ContentVerify("Books");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Computers"), Order(1)]
        public void CheckComputers()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Computers");
            explorer.OpenPage("computers");
                    test.Info($"Проверка перехода в категорию Computers");
            explorer.UrlVerify("http://demowebshop.tricentis.com/computers");
            explorer.ContentVerify("Computers");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка подвкладки Notebooks"), Order(2)]
        public void CheckComputers_Notebooks()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Computers -> Notebooks");
            explorer.OpenPageWithList("computers", "notebooks");
                    test.Info($"Проверка перехода в категорию Computers -> Notebooks");
            explorer.UrlVerify("http://demowebshop.tricentis.com/notebooks");
            explorer.ContentVerify("Notebooks");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка подвкладки Desktops"), Order(3)]
        public void CheckComputers_Desktops()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                     test.Info($"Переход в категорию Computers -> Desktops");
            explorer.OpenPageWithList("computers", "desktops");
                    test.Info($"Проверка перехода в категорию Computers -> Desktops");
            explorer.UrlVerify("http://demowebshop.tricentis.com/desktops");
            explorer.ContentVerify("Desktops");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка подвкладки Accessories"), Order(4)]
        public void CheckComputers_Accessories()
        {
            test = extent.CreateTest( DescriptionAttribute.value);
                    test.Info($"Переход в категорию Computers -> Accessories");
            explorer.OpenPageWithList("computers", "accessories");
                    test.Info($"Проверка перехода в категорию Computers -> Accessories");
            explorer.UrlVerify("http://demowebshop.tricentis.com/accessories");
            explorer.ContentVerify("Accessories");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Electronics"), Order(5)]
        public void CheckElectronics()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Electronics");
            explorer.OpenPage("electronics");
                    test.Info($"Проверка перехода в категорию Electronics");
            explorer.UrlVerify("http://demowebshop.tricentis.com/electronics");
            explorer.ContentVerify("Electronics");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка подвкладки Camera, photo"), Order(6)]
        public void CheckElectronics_Camera_Photo()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Electronics -> Camera, photo");
            explorer.OpenPageWithList("electronics", "camera-photo");
                    test.Info($"Проверка перехода в категорию Electronics -> Camera, photo");
            explorer.UrlVerify("http://demowebshop.tricentis.com/camera-photo");
            explorer.ContentVerify("Camera, photo");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Cell phones"), Order(7)]
        public void CheckElectronics_CellPhones()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Electronics -> Cell phones");
            explorer.OpenPageWithList("electronics", "cell-phones");
                    test.Info($"Проверка перехода в категорию Electronics -> Cell phones");
            explorer.UrlVerify("http://demowebshop.tricentis.com/cell-phones");
            explorer.ContentVerify("Cell phones");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Apparel&Shoes"), Order(8)]
        public void CheckApparelShoes()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Apparel & Shoes");
            explorer.OpenPage("apparel-shoes");
                    test.Info($"Проверка перехода в категорию Apparel & Shoes");
            explorer.UrlVerify("http://demowebshop.tricentis.com/apparel-shoes");
            explorer.ContentVerify("Apparel & Shoes");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Digital downloads"), Order(9)]
        public void CheckDigitalDownloades()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Digital downloads");
            explorer.OpenPage("digital-downloads");
                    test.Info($"Проверка перехода в категорию Digital downloads");
            explorer.UrlVerify("http://demowebshop.tricentis.com/digital-downloads");
            explorer.ContentVerify("Digital downloads");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Jewelry"), Order(10)]
        public void CheckJewelry()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Jewelry");
            explorer.OpenPage("jewelry");
                    test.Info($"Проверка перехода в категорию Jewelry");
            explorer.UrlVerify("http://demowebshop.tricentis.com/jewelry");
            explorer.ContentVerify("Jewelry");
                    test.Pass($"Тест завершен");
        }
        [Test, Description("Проверка вкладки Gift Cards"), Order(11)]
        public void CheckGiftCards()
        {
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Переход в категорию Gift Cards");
            explorer.OpenPage("gift-cards");
                    test.Info($"Проверка перехода в категорию Gift Cards");
            explorer.UrlVerify("http://demowebshop.tricentis.com/gift-cards");
            explorer.ContentVerify("Gift Cards");
                    test.Pass($"Тест завершен");
        }
    }
}
