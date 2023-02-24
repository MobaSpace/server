using System;
using System.Collections.Generic;
using System.Text;

namespace MobaSpace.Core.Email
{
    public class EmailSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string SenderEmail { get; set; }
    }
}
