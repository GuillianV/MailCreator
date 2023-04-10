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


        private Matiere _matiere;
        private Professeur? _professeur;

        public Relance(Matiere matiere, Professeur? professeur)
        {
            _matiere = matiere;
            Promo = matiere.Promo;
            NomMatiere = matiere.Nom;
            Salle = matiere.Salle;
            Jour = matiere.Jour;
            Seance = matiere.Seance;
            Visioconference = matiere.Visioconference;

            if(professeur != null)
            {
                
                _professeur = professeur;
                EstRelancable = true;
                EnseignantCivilite = professeur.Civilite;
                EnseignantNom = professeur.Nom;
                EnseignantPrenom = professeur.Prenom;
                EnseignantMail= professeur.Mail;
            }

        }

    }
}
