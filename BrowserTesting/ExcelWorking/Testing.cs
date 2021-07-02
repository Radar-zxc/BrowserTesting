using NUnit.Framework;

namespace BrowserTesting.ExcelWorking
{
    
    class Testing
    {
        [Test][Description("Добавление текста в ячейки, создание и удаление нового листа")]
        public void StartTest()
        {
            ExcelInfo.Add_Fill_Delete();
        }
    }
}
