using NUnit.Framework;

namespace BrowserTesting
{
    class DragAndDrop : TestBase
    {
        private DraggableElements elements;
        public override void DriverSetUp()
        {
            Driver.Navigate().GoToUrl("https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_draggable");
        }
        [OneTimeSetUp]
        public void Prepare()
        {
            elements = new DraggableElements(Driver);
        }
        [Test, Description("Check Drag and Drop functionality on special site")]
        public void CheckDragAndDrop()
        { 
            test = extent.CreateTest(DescriptionAttribute.value);
                    test.Info($"Открыта базовая страница");
                    test.Info($"Начало проверки функции перетаскиваиня элемента на странице с использованием Drag and Drop");
            elements.StartCheck();
                    test.Info($"Начало проверки функции перетаскиваиня элемента на странице без использования Drag and Drop");
            elements.StartDragDrop_Primitive();
                    test.Info($"Проверка клика по элементу с использованием JS кода ");
            elements.ClickWithJavaScript();
                    test.Pass($"Тест завершен");
        }
    }
}
