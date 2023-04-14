using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class EmailProperty
    {

        public string Nom { get; set; }

        public string MatchText { get; set; }
        public string Traduction { get; set; }

        public EmailProperty(string _nom,string _matchText,string traduction)
        {
            Nom = _nom;
            MatchText = _matchText;
            Traduction = traduction;
        }

    }
}
