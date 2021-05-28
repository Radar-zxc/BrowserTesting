using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserTesting
{
    public static class InputPreform
    {
        public static Dictionary<string, string> preforms = new Dictionary<string, string>()
        {
            ["valid"] = "Science",
            ["lessThanMinValid"] = "bo",
            ["minValid"] = "boo",
            ["invalid"] = "zp9",
            ["max"] = "",
            ["validForSubCategory"] = "desk",
            ["validDescription"] = "must",
        };
    }
}
