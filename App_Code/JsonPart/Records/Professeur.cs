using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonPart.Records
{
    public class Professeur
    {
        public string Civilite { get; set; }
        public string Name { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }

        public Professeur(string _civilite,string _name, string _prenom, string _mail)
        {
            this.Civilite = _civilite;
            this.Name = _name;
            this.Prenom = _prenom;
            this.Mail = _mail;
        }

    }
}
