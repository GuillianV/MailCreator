using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Matiere
    {
        public string Promo { get; private set; }

        public string Nom { get; private set; }

        public string Enseignant { get; private set; }

        public string Salle { get; private set; }

        public string Jour { get; private set; }

        public string Seance { get; private set; }

        public bool Visioconference { get; private set; }

        public Matiere(string promo, string matiere, string enseignant, string salle, string jour, string seance ,bool visioconference = false)
        {
            Promo = promo;
            Nom = matiere;
            Enseignant = enseignant;
            Salle = salle;
            Jour = jour;
            Seance = seance;
            Visioconference = visioconference;
        }

    }
}
