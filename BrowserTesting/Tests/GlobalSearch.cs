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
    /// <summary>
    /// KEYWORD:
    /// проверка пустого запроса со стартовой страницы
    /// проверка пустого запроса на странице поиска
    /// проверка запроса меньше 3 символов со стартовой страницы
    /// проверка запроса из трех символов со стартовой страницы
    /// запроса меньше 3 символов 
    /// запроса из трех символов
    /// поиск предмета который есть по такому запросу
    /// поиск предмета которого нет по такому запросу
    /// проверка очень большого запроса (сайт уже сломался) на 373 символа и падаем работает только со страницы поиска
    /// проверка очень большого запроса (сайт уже сломался) на 392 символа и падаем работает только со стартовой
    /// проверка запроса больше чем из 1 слова
    /// проверка запроса в зависимости от регистра  (должно быть независимо)
    /// 
    /// то что сверху по большей части это буллщит 
    /// можно проверить то появляющееся окно при пустом поиске из стартовой страницы
    /// сделать примерно 5 тестовых методов для проверки расширенного поиска 
    /// КАЖДУЮ КАТЕГОРИЮ ПРОВЕРЯТЬ НЕ НУЖНО 
    /// проверяем функциональность по факту без привязки к конкретному элементу 
    /// </summary>
    class GlobalSearch : TestBase
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
    }
}
