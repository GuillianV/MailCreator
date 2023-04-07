using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView.Entries
{
    public static class Jours
    {

        public static List<Jour> GetJours()
        {
            return new List<Jour>() {
             new Jour("", new List<string>()),
             new Jour("LUNDI", new List<string>() { "C", "D" }),
             new Jour("MARDI", new List<string>() { "F", "G" }),
             new Jour("MERCREDI", new List<string>() { "I", "J" }),
             new Jour("JEUDI", new List<string>() { "L", "M" }),
             new Jour("VENDREDI", new List<string>() { "O", "P" }),};
        }


        public static Jour GetJourByColumn(string ColumnReference)
        {
           
            List<Jour> jours = new List<Jour>() {
             new Jour("", new List<string>()),
             new Jour("LUNDI", new List<string>() { "C", "D" }),
             new Jour("MARDI", new List<string>() { "F", "G" }),
             new Jour("MERCREDI", new List<string>() { "I", "J" }),
             new Jour("JEUDI", new List<string>() { "L", "M" }),
             new Jour("VENDREDI", new List<string>() { "O", "P" }),};

            Jour jourTrouve = jours.First();

            jours.ForEach(jour =>
            {
                if (jour.ColumnReference.Contains(ColumnReference))
                    jourTrouve = jour;


            });

            return jourTrouve;
        }


    }
}
