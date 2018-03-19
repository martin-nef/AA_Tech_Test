using AA_Tech_Test;
using AA_Tech_Test.Exceptions;
using AA_Tech_Test.Types;
using AA_Tech_Test.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace Tests
{
    [TestClass]
    public class WebFormToolsTests
    {
        [TestMethod]
        public void PostFormDataTest()
        {
            FormData formData = new FormData("Contact Name", "Contact Email", "Contact Subject", "Message");
            string reference = WebFormTools.PostFormData(formData);
            Console.WriteLine($"PostFormDataTest, reference={reference}.");
            if (reference.Length == 0 || reference.Length > 30)
            {
                throw new InvalidReferenceException();
            }
        }
    }
}