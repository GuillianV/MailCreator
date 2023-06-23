using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.Model;

namespace MailCreator.App_Code.Utils
{
    public static class TelerikExtensions
    {


        public static string RtbToHtml(RadRichTextBox rtb)
        {
            HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();

            // Exportez le contenu de RadRichTextBox en tant que chaîne HTML
            string htmlContent = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                htmlFormatProvider.Export(rtb.Document, ms);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    htmlContent = sr.ReadToEnd();
                }
            }

            return htmlContent;
        }

        public static string RtbToHtml(List<RadRichTextBox> rtbs)
        {
            HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();

            // Exportez le contenu de RadRichTextBox en tant que chaîne HTML
            string htmlContent = string.Empty;
          


            if (rtbs != null)
            {
                rtbs.ForEach(rtb =>
                {
                    MemoryStream ms = new MemoryStream();
                    htmlFormatProvider.Export(rtb.Document, ms);
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    htmlContent += sr.ReadToEnd();

                });
            }


            return htmlContent;
        }

        public static RadDocument HtmlToRtb(string html)
        {
            HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();

            RadDocument document = new RadDocument();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(html);
                ms.Write(byteArray, 0, byteArray.Length);
                ms.Position = 0;
                document = htmlFormatProvider.Import(ms);
            }

            return document;
        }

    }
}
