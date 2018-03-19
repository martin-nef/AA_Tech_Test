using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AA_Tech_Test.Types
{
    public class FormData
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactSubject { get; set; }
        public string Message { get; set; }

        public FormData()
        {
            ContactName = string.Empty;
            ContactEmail = string.Empty;
            ContactSubject = string.Empty;
            Message = string.Empty;
        }

        public FormData(string contactName, string contactEmail, string contactSubject, string message)
        {
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactSubject = contactSubject;
            Message = message;
        }
    }
}
