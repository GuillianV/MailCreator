using Excel;
using ExcelPart;
using ExcelPart.UI;
using Json;
using MailCreator.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;
//using Excel = Microsoft.Office.Interop.Excel;
using Packaging = DocumentFormat.OpenXml.Packaging;
using Spreadsheet = DocumentFormat.OpenXml.Spreadsheet;


namespace MailCreator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            HomeWindow mainWindow = new HomeWindow();
            MainFrame.Navigate(mainWindow);
        }

        public async void ShowPopupSucces(PopupValue popupValue)
        {
            popup.IsOpen = true;
            bPopup.Background = new SolidColorBrush(popupValue.BackgroundColor);
            txtPopup.Foreground =new SolidColorBrush(popupValue.ForegroundColor );
            txtPopup.Text = popupValue.Name;
            await Task.Delay((int)popupValue.PopupTime);
            popup.IsOpen = false;
        }


    }
}
