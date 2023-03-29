using Excel;
using ExcelPart;
using ExcelPart.UI;
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
        List<WeekViewBinding> weekViewBinding = new List<WeekViewBinding>();
        public void panExcelSemaine_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileInfo? fileInfo = IsDropAllowed(files, bordExcelSemaine, lbDropExcelSemaine);
                if (fileInfo!= null)
                {
                    ExcelManager excel = new ExcelManager(fileInfo);
                    ExcelWeekParser excelWeekParser = new ExcelWeekParser(excel.ShowCellsValues());

                    WeekWindow mainWindow = new WeekWindow(excelWeekParser);
                    this.Content = mainWindow;

                }

            }
        }

        private void dgCells_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgCells.SelectedValue?.GetType() == typeof(WeekViewBinding))
            {

                WeekViewBinding week = (WeekViewBinding)dgCells.SelectedValue;
                dgCells.ItemsSource = week.Matieres;

            }

            
        }
    }
}
