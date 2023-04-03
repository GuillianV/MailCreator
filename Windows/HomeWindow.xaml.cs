using Excel;
using ExcelPart;
using ExcelPart.UI;
using Json;
using JsonPart.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


namespace MailCreator.Windows
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : UserControl
    {

        public HomeWindow()
        {
            InitializeComponent();
        }

        private FileInfo? IsDropAllowed(String[] filesName, Border border, Label content)
        {
            if (filesName.Length == 1)
            {

                FileInfo file = new FileInfo(filesName[0]);
                if (!file.IsReadOnly && file.Extension.Contains("xls"))
                {

                    border.BorderThickness = new Thickness(2);
                    border.BorderBrush = new SolidColorBrush(Colors.Green);
                    content.Content = "Drop it !";

                    return file;
                }

            }

            border.BorderThickness = new Thickness(2);
            border.BorderBrush = new SolidColorBrush(Colors.Red);
            content.Content = "Type de fichier non authorisé";

            return null;

        }

        public void panExcelSemaine_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                IsDropAllowed(files, bordExcelSemaine, lbDropExcelSemaine);

            }


        }
        public void panExcelSemaine_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileInfo? fileInfo = IsDropAllowed(files, bordExcelSemaine, lbDropExcelSemaine);
                if (fileInfo != null)
                {
                    ExcelManager excel = new ExcelManager(fileInfo);
                    ExcelWeekParser excelWeekParser = new ExcelWeekParser(excel.ShowCellsValues());

                    WeekWindow mainWindow = new WeekWindow(excelWeekParser);
                    this.Content = mainWindow;

                }

            }
        }

      
    }

}
