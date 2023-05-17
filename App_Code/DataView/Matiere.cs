using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class Matiere
    {
        public string Promo { get; set; }

        public string NombreEtudiants { get; set; }

        public string Nom { get; set; }

        public string Enseignant { get; set; }

        public string Salle { get; set; }

        public string Jour { get; set; }

        public string Seance { get; set; }

        public bool Visioconference { get; set; }

        public Matiere(string promo,string nombreEtudiants, string matiere, string enseignant, string salle, string jour, string seance ,bool visioconference = false)
        {
            Promo = promo;
            NombreEtudiants = nombreEtudiants;
            Nom = matiere;
            Enseignant = enseignant;
            Salle = salle;
            Jour = jour;
            Seance = seance;
            Visioconference = visioconference;
        }

    }
}
