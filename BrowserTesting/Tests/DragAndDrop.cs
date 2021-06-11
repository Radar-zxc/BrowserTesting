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
            elements.StartCheck();
            elements.StartDragDrop_Primitive();
            elements.ClickWithJavaScript();
        }
    }
}
