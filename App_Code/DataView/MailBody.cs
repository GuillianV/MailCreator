using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCreator.App_Code.DataView
{
    public struct MailBody
    {

        public string Header;
        public string Body;
        public string Footer;

        public MailBody(string _header, string _body, string _footer)
        {
            Header = _header;
            Body = _body;
            Footer = _footer;

        }

        public string GetRowText()
        {
            return Header + Body + Footer;
        }

        public string GetHeader()
        {
            return Header;
        }

        public string GetBody()
        {
            return Body;
        }

        public string GetFooter()
        {
            return Footer;
        }

    }
}
