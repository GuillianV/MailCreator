using MailCreator.App_Code.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class EmailGeneric
    {

        public string Sujet { get; set; }
        public MailBody Body { get; set; }

        public EmailGeneric(string _sujet, MailBody _body)
        {
            this.Sujet = _sujet;
            this.Body = _body;
        }

    }
}
