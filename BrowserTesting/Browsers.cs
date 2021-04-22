using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using System.Text.Json;
namespace BrowserTesting
{
    public class Browsers
    {
        class Browser
        {
            public string name { get; set; }
        }
       public class JsonRead
       {
             public static string Read_file ()
             {
                FileStream fs = new FileStream("Appsettings.json", FileMode.Open);
                byte [] array = new byte[fs.Length];
                fs.Read(array);
                string info = System.Text.Encoding.Default.GetString(array);
                Browser restoredBrowser = JsonSerializer.Deserialize<Browser>(info);
                return restoredBrowser.name;
             }
        }
    }
}
