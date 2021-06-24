using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserTesting
{
    public struct GiftCardInputInfo
    {
        public string recName;
        public string sendName;
        public string recEmail;
        public string sendEmail;
        public GiftCardInputInfo(string recName, string sendName, string recEmail, string sendEmail)
        {
            this.recName = recName;
            this.sendName = sendName;
            this.recEmail = recEmail;
            this.sendEmail = sendEmail;
        }
    }
}
