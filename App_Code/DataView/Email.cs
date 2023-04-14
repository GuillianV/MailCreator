using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Email
    {

        public string Sujet { get; set; }
        public string Body { get; set; }

        public Email(string _sujet,string _body)
        {
            this.Sujet = _sujet;
            this.Body = _body;
        }

    }
}
