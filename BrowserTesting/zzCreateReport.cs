using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserTesting.zzCreateReport
{
    
    class zzCreateReport:TestBase
    {
        [Test]
        public void CreateReport()
        {
            extent.Flush();
        }
    }
}
