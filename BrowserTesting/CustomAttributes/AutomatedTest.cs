using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserTesting
{
    /// <summary>
    /// Класс, содержащий определение для аттрибута AutomatedTest
    /// </summary>
    public class AutomatedTestAttribute : System.Attribute
    {
        public static int value;
        public AutomatedTestAttribute(int x)
        {
             value = x;
        }
    }
    public class DescriptionAttribute : System.Attribute
    {
        public static string value;
        public DescriptionAttribute(string x)
        {
            value = x;
        }
    }
}
