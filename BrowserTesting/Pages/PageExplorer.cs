using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace BrowserTesting.Pages
{
    /// <summary>
    /// Класс, содержащий свойства и методы, необходимые для осуществления
    /// переходов между страницами и вкладками
    /// </summary>
    class PageExplorer : BasePage
    {
        private List<string> tabsList;
        public PageExplorer(IWebDriver Driver):base(Driver)
        {
        }
        /// <summary>
        /// Метод открытия страницы с указаным названием из заголовка сайта
        /// </summary>
        public void OpenPage(string pageName)
        {
            string path = $".top-menu a[href='/{ pageName}']";
            ClickOnElement(By.CssSelector(path));
        }
        /// <summary>
        /// Метод открытия страницы из заголовка сайта , находящейся в выпадающем списке 
        /// </summary>
        public void OpenPageWithList(string pageName, string pageElement)
        {
            string path = $".top-menu a[href='/{ pageName}']";
            var find = Driver.EFindElement(By.CssSelector(path));
            Driver.MoveToElement(find);
            path = $"ul.active a[href='/{ pageElement}']";
            ClickOnElement(By.CssSelector(path));
        }
        /// <summary>
        /// Метод открытия стартовой страницы сайта Tricentis Demo Web Shop путем нажатия на логотип
        /// </summary>
        public void OpenStartPage()
        {
            string path = "img[alt='Tricentis Demo Web Shop']";
            ClickOnElement(By.CssSelector(path));
        }
        /// <summary>
        /// Метод открытия страницы корзины
        /// </summary>
        public void OpenCart()
        {
            By cart = By.CssSelector("a.ico-cart>span.cart-label");
            ClickOnElement(cart);
        }
        /// <summary>
        /// Метод открытия страницы Wishlist
        /// </summary>
        public void OpenWishlist()
        {
            ClickOnElement(By.CssSelector("a.ico-wishlist>span.cart-label"));
        }
        /// <summary>
        /// Метод открытия страницы товара из списка по заданому имени 
        /// </summary>
        public void GoToItemPage(string itemName)
        {
            string pathItem = $"img[alt*='{itemName}']";
            ClickOnElement(By.CssSelector(pathItem));
        }
        /// <summary>
        /// Метод проверки перехода на страницу корзины 
        /// </summary>
        public void CheckCartTravel()
        {
            UrlVerify(CartPage.cartUrl);
        }
        /// <summary>
        /// Метод открытия страницы в новой вкладке по заданному имени
        /// </summary>
        public void OpenPageInNewTab(string pageName)
        {
            By newPage = null;
            if (Driver.FindElements(By.XPath($"//div[@class='header-links']//a[contains(@href,'{pageName}')]")).Count != 0)
            {
                newPage = By.XPath($"//div[@class='header-links']//a[contains(@href,'{pageName}')]");
            }else if (Driver.FindElements(By.XPath($"//ul[@class='top-menu']//a[contains(@href,'{pageName}')]")).Count != 0)
            {
                newPage = By.XPath($"//ul[@class='top-menu']//a[contains(@href,'{pageName}')]");
            }else if (Driver.FindElements(By.XPath($"//div[@class='center-1']//a[@class='{pageName}']")).Count != 0)
            {
                newPage = By.XPath($"//div[@class='center-1']//a[@class='{pageName}']");
            }
            else
            {
                throw new System.Exception($"Объект с именем {pageName} не найден на странице ");
            }
            OpenPageRef(newPage);
            RefreshTabsList();
            Driver.SwitchTo().Window(tabsList.Last());
            if (WindowOptions.WindowAutoMaxSize)
            {
                Driver.Manage().Window.Maximize();
            }
            else
            {
                Driver.Manage().Window.Size = new System.Drawing.Size(WindowOptions.Window_x, WindowOptions.Window_y);
            }
        }
        public void OpenTopMenuInNewTab(string pageName)
        {
            string path = $"//div[@class='header-links']//a[contains(@href,'{pageName}')]";
            By newPage = By.XPath(path);
            OpenPageRef(newPage);
            RefreshTabsList();
            Driver.SwitchTo().Window(tabsList.Last());
            if (WindowOptions.WindowAutoMaxSize)
            {
                Driver.Manage().Window.Maximize();
            }
            else
            {
                Driver.Manage().Window.Size = new System.Drawing.Size(WindowOptions.Window_x, WindowOptions.Window_y);
            }
        }
        /// <summary>
        /// Метод обновляющий весь список открытых вкладок
        /// </summary>
        private void RefreshTabsList()
        {
            tabsList = Driver.WindowHandles.ToList();
        }
        /// <summary>
        /// Метод перехода на вкладку с заданной очередностью ее открытия
        /// </summary>
        public void GoToTab(int tabNubmer)
        {
            RefreshTabsList();
            Assert.IsTrue(tabNubmer < tabsList.Count && tabNubmer >= 0,
                "Попытка обращения к вкладке, которая не могла существовать");
            bool found=false;
            foreach (string tab in tabsList)
            {
                if (tab == tabsList[tabNubmer])
                {
                    Driver.SwitchTo().Window(tab);
                    Driver.EFindElement(By.CssSelector("body")).Click();
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found,"Попытка обращения к удаленнной вкладке");
        }
        /// <summary>
        /// Метод, сравнивающий текущий URL с передаваемым в качестве параметра, 
        /// предусмотрен Assert при несоответствии
        /// </summary>
        public void UrlVerify(string necessaryUrl)
        {
            Asserts.UrlVerify(Driver, necessaryUrl);
        }
        /// <summary>
        /// Метод проверки открытия вкладки с заданным именем
        /// </summary>
        public void ContentVerify(string key)
        {
            Asserts.ContentVerify(Driver, key);
        }
        /// <summary>
        /// Метод закрытия текущего окна браузера
        /// </summary>
        public void CloseCurrentWindow()
        {
            var lastActiveWindow = Driver.CurrentWindowHandle;
            Driver.Close();
            if (tabsList.Count == tabsList.IndexOf(lastActiveWindow) + 1)
            {
                Driver.SwitchTo().Window(tabsList[tabsList.IndexOf(lastActiveWindow) - 1]);
            }
            else
            {
                Driver.SwitchTo().Window(tabsList[tabsList.IndexOf(lastActiveWindow) + 1]);
            }
            tabsList.Remove(lastActiveWindow);
        }
        /// <summary>
        /// Метод закрытия окна браузера по заданному номеру 
        /// </summary>
        public void CloseWindow(int number)
        {
            var lastActiveWindow = Driver.CurrentWindowHandle;
            if (tabsList.IndexOf(lastActiveWindow) == number)
            {
                CloseCurrentWindow();
            }
            else
            {
                Driver.SwitchTo().Window(tabsList[number]);
                Driver.Close();
                tabsList.Remove(tabsList[number]);
                Driver.SwitchTo().Window(lastActiveWindow);
            }
        }
    }
}
