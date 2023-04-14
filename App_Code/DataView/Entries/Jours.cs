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
             new Jour("", new List<string>(),0),
             new Jour("LUNDI", new List<string>() { "C", "D" },0),
             new Jour("MARDI", new List<string>() { "F", "G" },1),
             new Jour("MERCREDI", new List<string>() { "I", "J" },2),
             new Jour("JEUDI", new List<string>() { "L", "M" },3),
             new Jour("VENDREDI", new List<string>() { "O", "P" },4),};
        }


        public static Jour GetJourByColumn(string ColumnReference)
        {
           
            List<Jour> jours = new List<Jour>() {
             new Jour("", new List<string>(),0),
             new Jour("LUNDI", new List<string>() { "C", "D" },0),
             new Jour("MARDI", new List<string>() { "F", "G" },1),
             new Jour("MERCREDI", new List<string>() { "I", "J" },2),
             new Jour("JEUDI", new List<string>() { "L", "M" },3),
             new Jour("VENDREDI", new List<string>() { "O", "P" },4)};

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
