using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class EmailData
    {
        public string EmailToSend { get; set; }
        public string UserEmail { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }

        public EmailData(string emailToSend, string userEmail, string header, string body)
        {
            EmailToSend = emailToSend;
            UserEmail = userEmail;
            Header = header;
            Body = body;
        }
    }
}
