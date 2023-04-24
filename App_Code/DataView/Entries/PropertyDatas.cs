using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView.Entries
{
    public static class PropertyDatas
    {
        public static PropertyData Promo = new PropertyData("Promo", "$promo$", "Promotion");
        public static PropertyData NomMatiere = new PropertyData("NomMatiere", "$nomMatiere$", "Nom de la matière");
        public static PropertyData Salle = new PropertyData("Salle", "$salle$", "Salle");
        public static PropertyData Jour = new PropertyData("Jour", "$jour$", "Jour");
        public static PropertyData Seance = new PropertyData("Seance", "$seance$", "Seance");
        public static PropertyData Date = new PropertyData("Date", "$date$", "Date");
        public static PropertyData Visioconference = new PropertyData("Visioconference", "$visioconference$", "Le cours se passe en visioconference");
        public static PropertyData EnseignantCivilite = new PropertyData("EnseignantCivilite", "$enseignantCivilite$", "Civilité de l'enseignant");
        public static PropertyData EnseignantNom = new PropertyData("EnseignantNom", "$enseignantNom$", "Nom de l'enseignant");
        public static PropertyData EnseignantPrenom = new PropertyData("EnseignantPrenom", "$enseignantPrenom$", "Prénom de l'enseignant");
        public static PropertyData EnseignantMail = new PropertyData("EnseignantMail", "$enseignantMail$", "Mail de l'enseignant");

        private static List<PropertyData> _propertyDatas = new List<PropertyData>() { Promo ,NomMatiere,Salle,Jour,Seance,Date,Visioconference,EnseignantCivilite,EnseignantMail,EnseignantNom,EnseignantPrenom};

        public static PropertyData GetPropertyDataByName(string identifier)
        {
            return _propertyDatas.FirstOrDefault(p => p.Nom == identifier);
            
        }
    }
}
