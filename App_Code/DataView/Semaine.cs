using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Semaine
    {

        public string Nom { get; set; }

        public List<DateTime> Dates { get; set; }

        public List<Matiere> Matieres { get; set; }

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
