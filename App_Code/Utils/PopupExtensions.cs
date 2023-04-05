using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Utils
{
    public static class PopupExtensions
    {
        public static PopupValue EnregistrerSucces = new PopupValue("Enregistrement Effectué", Colors.LightGreen, Colors.Black, PopupType.Succes,PopupTime.Medium);
        public static PopupValue EnregistrerFail = new PopupValue("Echec d'Enregistrement", Colors.DarkRed, Colors.White, PopupType.Failure, PopupTime.Medium);

    }


    public class PopupValue
    {
        public string Name { get; private set; }
        public Color BackgroundColor { get; private set; }
        public Color ForegroundColor { get; private set; }
        public PopupType PopupType { get; private set; }
        public PopupTime PopupTime { get; private set; }

        public PopupValue(string _name, Color _backgroundColor, Color _foregroundColor, PopupType _popupType, PopupTime _popupTime)
        {

            Name = _name;
            BackgroundColor = _backgroundColor;
            ForegroundColor = _foregroundColor;
            PopupType = _popupType;
            PopupTime = _popupTime;

        }
    }

    public enum PopupType
    {
        Succes,
        Failure
    }

    public enum PopupTime
    {
        Short = 1000,
        Medium = 2000,
        Long = 5000,
    }

}
