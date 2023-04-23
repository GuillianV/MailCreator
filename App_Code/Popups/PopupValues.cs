using Popups.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Popups
{
    public static class PopupValues
    {
        //Succes
        public static PopupValue EnregistrerSucces = new PopupValue("Enregistrement Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);
        public static PopupValue EnvoiSucces = new PopupValue("Envoi Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);
        public static PopupValue SuppressionSucces = new PopupValue("Suppression Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);
        public static PopupValue ModificationSucces = new PopupValue("Modification Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);
        public static PopupValue CreationSucces = new PopupValue("Création Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);

        //Warn
        public static PopupValue MissingSelectValueWarn = new PopupValue("Séléctionnez un objet dans la liste avant de continuer", Colors.Orange, Colors.Black, PopupType.Warn, PopupTime.Medium);

        //Failed
        public static PopupValue EnregistrerFail = new PopupValue("Echec d'Enregistrement", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue SupprimerFail = new PopupValue("Echec de la Suppression", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue EnvoiFail = new PopupValue("Echec de l'envoi", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue ModificationFail = new PopupValue("Echec de la Modification", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue BindingFail = new PopupValue("Impossible d'afficher l'élement", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue CreationFail = new PopupValue("Impossible de créer l'élement", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);
        public static PopupValue ConnexionOutlookFail = new PopupValue("Impossible de se connecter à Outlook.", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);

    }
}
