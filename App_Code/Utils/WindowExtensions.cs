using MailCreator;
using Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utils
{
    public static class WindowExtensions
    {

        public static void ShowPopup(this DependencyObject windowExtensions, PopupValue popupValue)
        {
            // Obtenez la fenêtre parent (MainWindow) du User Control
            Window parentWindow = Window.GetWindow(windowExtensions);
            if (parentWindow.GetType() == typeof(MainWindow))
            {
                MainWindow mainWindow = (MainWindow)parentWindow;
                mainWindow.ShowPopupSucces(popupValue);
            }

        }

    }
}
