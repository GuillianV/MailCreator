using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Semaine
    {

        public string Nom { get; private set; }

        public List<DateTime> Dates { get; private set; }

        public List<Matiere> Matieres { get; private set; }

        public Semaine(string semaine)
        {
            Nom = semaine;

        }


        public void SetMatieres(List<Matiere> matieres)
        {
            Matieres = matieres;
        }


        public void SetDates(List<DateTime> dates)
        {
            Dates = dates;
        }


    }
}
