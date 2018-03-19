using AA_Tech_Test.Exceptions;
using AA_Tech_Test.Types;
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

namespace AA_Tech_Test.Utilities
{
    public static class WebFormTools
    {
        private static readonly HttpClient client = new HttpClient();

        public static string PostFormData(FormData formData)
        {
            var awaiter = PostFormDataAsync(formData).GetAwaiter();
            while (awaiter.IsCompleted == false)
            {
            }
            return awaiter.GetResult();
        }

        public static async Task<string> PostFormDataAsync(FormData formData)
        {
            string reference = string.Empty;
            string formUrl = "https://agileautomations.co.uk/home/inputform";

            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(formUrl);
            postRequest.Headers.Add("ContactName", formData.ContactName);
            postRequest.Headers.Add("ContactEmail", formData.ContactEmail);
            postRequest.Headers.Add("ContactSubject", formData.ContactSubject);
            postRequest.Headers.Add("Message", formData.Message);
            postRequest.Headers.Add("ReferenceNo", "");
            postRequest.ContentType = "multipart/form-data";
            postRequest.Accept = "text/*";
            WebResponse formResponse = await postRequest.GetResponseAsync();
            StreamReader reader = new StreamReader(formResponse.GetResponseStream());
            return await reader.ReadToEndAsync();
        }
    }
}
