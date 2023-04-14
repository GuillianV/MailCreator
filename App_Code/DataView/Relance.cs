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
        public EmailProperty Promo { get; set; }
        public EmailProperty NomMatiere { get; set; }
        public EmailProperty Salle { get; set; }
        public EmailProperty Jour { get; set; }
        public EmailProperty Seance { get; set; }

        public EmailProperty Date { get; set; }
        public bool EstVisioconference { get; set; }
        public EmailProperty Visioconference { get; set; }
        public EmailProperty EnseignantCivilite { get; set; }
        public EmailProperty EnseignantNom { get; set; }
        public EmailProperty EnseignantPrenom { get; set; }
        public EmailProperty EnseignantMail { get; set; }




        public Relance(bool estRelancable, bool estVisioconference, EmailProperty visioconference, EmailProperty promo, EmailProperty nomMatiere , EmailProperty salle, EmailProperty jour, EmailProperty date, EmailProperty seance, EmailProperty enseignantCivilite, EmailProperty enseignantNom , EmailProperty enseignantPrenom, EmailProperty enseignantMail)
        {
            EstRelancable = estRelancable;
            EstVisioconference = estVisioconference;
            Promo = promo;
            NomMatiere = nomMatiere;
            Salle = salle;
            Jour = jour;
            Date = date;
            Seance = seance;
            Visioconference = visioconference;
            EnseignantCivilite = enseignantCivilite;
            EnseignantNom = enseignantNom;
            EnseignantPrenom = enseignantPrenom;
            EnseignantMail = enseignantMail;

        }

   

    }
}
