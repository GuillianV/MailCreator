using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.DataView
{
    public class MailData
    {

        public string Destinataire { get; set; }
        public string Objet { get;  set; }
        public string Body { get;  set; }

        public MailData(string _destinataire, string _objet, string _body)
        {
            this.Destinataire = _destinataire;
            this.Objet = _objet;
            this.Body = _body;
        }

    }
}
