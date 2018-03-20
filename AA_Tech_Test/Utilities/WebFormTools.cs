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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace AA_Tech_Test.Utilities
{
    public static class WebFormTools
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Submit the form on the Agile Automation's website.
        /// </summary>
        /// <param name="formData">An object that holds the data necessary to submit the form.</param>
        /// <returns>A string reference number, tied to a unique submission.</returns>
        public static string PostFormData(FormData formData)
        {
            var awaiter = PostFormDataAsync(formData).GetAwaiter();
            while (awaiter.IsCompleted == false)
            {
            }
            return awaiter.GetResult();
        }

        /// <summary>
        /// Submit the form on the Agile Automation's website, asynchronously.
        /// </summary>
        /// <param name="formData">An object that holds the data necessary to submit the form.</param>
        /// <returns>A deferred task that will produce a string reference number, tied to a unique submission.</returns>
        public static async Task<string> PostFormDataAsync(FormData formData)
        {
            string reference = string.Empty;
            string xmlString = string.Empty;
            string formUrl = "https://agileautomations.co.uk/home/inputform";
            XmlDocument document;
            HttpWebRequest postRequest;
            HttpWebResponse formResponse;
            Stream stream;
            StreamReader reader;

            postRequest = (HttpWebRequest)WebRequest.Create(formUrl);
            postRequest.Headers.Add("ContactName", formData.ContactName);
            postRequest.Headers.Add("ContactEmail", formData.ContactEmail);
            postRequest.Headers.Add("ContactSubject", formData.ContactSubject);
            postRequest.Headers.Add("Message", formData.Message);
            postRequest.Headers.Add("ReferenceNo", "");
            postRequest.ContentType = "multipart/form-data";
            postRequest.Accept = "text/*";
            formResponse = (HttpWebResponse)await postRequest.GetResponseAsync();
            stream = formResponse.GetResponseStream();
            document = new XmlDocument();
            reader = new StreamReader(stream);

            try
            {
                xmlString = reader.ReadToEnd();
                document.LoadXml(xmlString);
                reference = document.GetElementById("ReferenceNo").InnerText;
            }
            catch (XmlException e)
            {
                // Try a fallback method in case previous method fails.
                Regex referenceFinder = new Regex(@"<label id=ReferenceNo>([^<>]*)<\/label>"); // find the right element
                Match match = referenceFinder.Match(xmlString);
                if (match.Success)
                {
                    reference = match.Groups[0].Value;
                }
                else
                {
                    throw new MyXmlException("Failed to parse the response upon submitting the form.", e, xmlString);
                }
            }
            catch (WebException e)
            {
                if (document == null)
                {
                    throw new WebException($"A network error has ocurred Web response status code was {formResponse.StatusCode} and description: {formResponse.StatusDescription}", e);
                }
            }

            if (!ReferenceValid(reference))
            {
                throw new InvalidReferenceException("Reference - 1234567", reference);
            }

            return reference;
        }
        
        public static bool ReferenceValid(string reference)
        {
            Regex referenceValidator = new Regex(@"Reference - ([0-9]{7})");
            Match match = referenceValidator.Match(reference);
            return match.Success;
        }
    }
}
