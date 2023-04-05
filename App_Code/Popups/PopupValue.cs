using Popups.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Popups
{
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
}
