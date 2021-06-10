using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Drawing;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий методы для выполнения действий с перетаскиваемыми элементами
    /// </summary>
    class DraggableElements : BasePage
    {
        public DraggableElements(IWebDriver Driver) : base(Driver)
        {

        }
        /// <summary>
        /// Метод нахождения элемента на странице по XPath 
        /// </summary>
        private IWebElement GetElem(string path)
        {
            return Driver.EFindElement(By.XPath(path));
        }
        /// <summary>
        /// Метод передвижения элемента к другому элементу
        /// </summary>
        void DragDropTo_Elem(IWebElement movableElem, IWebElement arrivalElem)
        {
            Actions action = new Actions(Driver);
            action.DragAndDrop(movableElem, arrivalElem).Build().Perform();
        }
        /// <summary>
        /// Метод передвижения элемента на дистанцию, равную значениям передаваемой точки
        /// </summary>
        void DragDropTo_ShiftValue(IWebElement movableElem, Point point)
        {
            Actions action = new Actions(Driver);
            action.DragAndDropToOffset(movableElem, point.X, point.Y)
                .Build().Perform();
        }
        /// <summary>
        /// Метод передвижения элемента на дистанцию, равную значениям передаваемой точки
        /// без использования метода DragAndDropToOffset
        /// </summary>
        void DragDropTo_ShiftValue_Primitive(IWebElement movableElem, Point point)
        {
            Actions action = new Actions(Driver);
            action.ClickAndHold(movableElem).MoveByOffset(point.X, point.Y)
                .Build().Perform();
        }
        /// <summary>
        /// Метод передвижения элемента к другому элементу без использования DragAndDrop 
        /// </summary>
        void DragDropTo_Elem_Primitive(IWebElement movableElem, IWebElement arrivalElem)
        {
            Actions action = new Actions(Driver);
            action.ClickAndHold(movableElem).MoveToElement(arrivalElem)
                .Release().Build().Perform();
        }
        /// <summary>
        /// Метод проверки сдвига элемента на значение 
        /// </summary>
        void CheckMoving_ShiftValue(IWebElement start,Point firstLotation, Point finish)
        {
            bool check = false;
            if (start.Location.X == firstLotation.X + finish.X
                && start.Location.Y == firstLotation.Y + finish.Y)
            {
                check = true;
            }
            Assert.IsTrue(check, "Передвижение элемента произошло неверно");
        }
        /// <summary>
        /// Метод проверки сдвига элемента к другому элементу
        /// </summary>
        void CheckMovingTo_Elem(IWebElement movableElem, IWebElement arrivalElem)
        {
            Size sizeArrival = arrivalElem.Size;
            Size sizeMovable = movableElem.Size;
            if (sizeMovable.Width < sizeArrival.Width || sizeMovable.Width > sizeArrival.Width)
            {
                Assert.IsTrue(movableElem.Location.Y == arrivalElem.Location.Y, "Передвижение элемента произошло неверно");
            }else if (sizeMovable.Height < sizeArrival.Height || sizeMovable.Height > sizeArrival.Height)
            {
                Assert.IsTrue(movableElem.Location.X == arrivalElem.Location.X, "Передвижение элемента произошло неверно");
            }else {
                Assert.IsTrue(movableElem.Location == arrivalElem.Location, "Передвижение элемента произошло неверно");
            }
        }
        /// <summary>
        /// Метод изменения текущего фрейма
        /// </summary>
        void ChangeFrame(string frameName)
        {
            Driver.SwitchTo().Frame(frameName);
        }
        /// <summary>
        /// Главная функция проверки перетаскивания элемента
        /// </summary>
        public void StartCheck()
        {
            ChangeFrame("iframeResult");
            IWebElement start = GetElem("//div[@id='mydivheader']");
            IWebElement finish = GetElem("//body[@contenteditable='false']//h1[contains(text(),'Drag')]");
            Point shiftValue = new Point(100, 100);
            Point firstLotation = start.Location;
            DragDropTo_ShiftValue(start, shiftValue);
            //CheckMoving_ShiftValue(start, firstLotation, shiftValue);
            DragDropTo_Elem(start,finish);
            //CheckMovingTo_Elem(start,finish);
        }
        /// <summary>
        /// Главная функция проверки перетаскивания элемента без использования DragAndDrop
        /// </summary>
        public void StartDragDrop_Primitive() 
        {
            IWebElement start = GetElem("//div[@id='mydivheader']");
            IWebElement finish = GetElem("//body[@contenteditable='false']//h1[contains(text(),'Drag')]");
            Point shiftValue = new Point(100, 100);
            DragDropTo_ShiftValue_Primitive(start, shiftValue);
            DragDropTo_Elem_Primitive(start, finish);
        }
        /// <summary>
        /// Метод клика по элементу с помощью JavaScript
        /// </summary>
        public void ClickWithJavaScript()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            Driver.SwitchTo().DefaultContent();
            executor.ExecuteScript("arguments[0].click();", Driver.EFindElement(By.XPath("//a[@id='menuButton']")));
        }
    }
}
