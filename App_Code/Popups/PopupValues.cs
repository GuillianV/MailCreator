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
        public static PopupValue EnregistrerSucces = new PopupValue("Enregistrement Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes, PopupTime.Medium);
        public static PopupValue EnregistrerFail = new PopupValue("Echec d'Enregistrement", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);

    }
}
