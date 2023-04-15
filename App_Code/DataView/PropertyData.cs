using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public  class PropertyData
    {
        public string Nom { get; set; }
        public string MatchText { get; set; }

        public string Description { get; set; }

        public PropertyData(string _nom, string _matchText, string _description)
        {
            Nom = _nom;
            MatchText = _matchText;
            Description = _description;
        }
    }
}
