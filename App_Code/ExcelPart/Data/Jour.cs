using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPart.Data
{
    public class Jour
    {
        public string Nom { get; private set; }
        public List<string> ColumnReference { get; private set; }

        public Jour(string nom, List<string> columnReference)
        {
            this.Nom = nom;
            this.ColumnReference = columnReference;
        }

    }
}
