using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPart.UI
{
    public class WeekViewBinding
    {

        public string Semaine { get; private set; }

        public List<DateTime> Dates { get; private set; }

        public List<MatiereViewBinding> Matieres { get; private set; }

        public WeekViewBinding(string semaine)
        {
            Semaine = semaine;

        }


        public void SetMatieres(List<MatiereViewBinding> matieres)
        {
            Matieres = matieres;
        }


        public void SetDates(List<DateTime> dates)
        {
            Dates = dates;
        }


    }
}
