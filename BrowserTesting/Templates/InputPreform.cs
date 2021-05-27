using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserTesting
{
    public class InputPreform
    {
        Dictionary<string, string> preforms = new Dictionary<string, string>()
        {
            ["normal"] = "Science",
            ["Empty"] = "",
            ["lessThanMinValid"] = "bo",
            ["minValid"] = "boo",
            ["invalid"] = "zp9",
            ["maxValid"] = "",
            ["maxValid+1"] = "",
        }
    }
}
