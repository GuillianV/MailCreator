using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPart.UI
{
    public class MatiereViewBinding
    {
        public string Promo { get; private set; }

        public string Matiere { get; private set; }

        public string Enseignant { get; private set; }

        public string Salle { get; private set; }

        public string Jour { get; private set; }

        public string Seance { get; private set; }

        public bool Visioconference { get; private set; }

        public MatiereViewBinding(string promo, string matiere, string enseignant, string salle, string jour, string seance ,bool visioconference = false)
        {
            Promo = promo;
            Matiere = matiere;
            Enseignant = enseignant;
            Salle = salle;
            Jour = jour;
            Seance = seance;
            Visioconference = visioconference;
        }

    }
}
