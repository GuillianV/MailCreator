using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Jour
    {
        public string Nom { get; private set; }
        public List<string> ColumnReference { get; private set; }

        public int AddDays { get; private set; }

        public Jour(string nom, List<string> columnReference, int addDays)
        {
            this.Nom = nom;
            this.ColumnReference = columnReference;
            this.AddDays = addDays;
        }

    }
}
