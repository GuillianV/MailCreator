using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataView
{
    public class Relance
    {
        public bool EstRelancable { get; set; }
        public string Promo { get; set; }
        public string NomMatiere { get; set; }
        public string Salle { get; set; }
        public string Jour { get; set; }
        public string Seance { get; set; }
        public bool Visioconference { get; set; }
        public string EnseignantCivilite { get; set; }
        public string EnseignantNom { get; set; }
        public string EnseignantPrenom { get; set; }
        public string EnseignantMail { get; set; }




        public Relance(bool estRelancable, string promo,string nomMatiere ,string salle, string jour,string seance, bool visioconference, string enseignantCivilite, string enseignantNom , string enseignantPrenom, string enseignantMail)
        {
            EstRelancable = estRelancable;
            Promo = promo;
            NomMatiere = nomMatiere;
            Salle = salle;
            Jour = jour;
            Seance = seance;
            Visioconference = visioconference;
            EnseignantCivilite = enseignantCivilite;
            EnseignantNom = enseignantNom;
            EnseignantPrenom = enseignantPrenom;
            EnseignantMail = enseignantMail;

        }

   

    }
}
