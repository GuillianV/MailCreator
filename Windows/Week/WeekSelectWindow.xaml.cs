using DataView;
using Excel;
using ExcelPart;
using Json;
using Popups;
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
using Utils;

namespace MailCreator.Windows.Week
{
    /// <summary>
    /// Logique d'interaction pour WeekWindow.xaml
    /// </summary>
    public partial class WeekSelectWindow : UserControl
    {
        public List<Matiere> Matieres { get; set; }
        private List<Semaine> Semaines { get; set; }

        private ExcelWeekParser _excelWeekParser;

        public WeekSelectWindow()
        {
            

            InitializeComponent();

      

        }


        private void BindWeekListView(List<Semaine> _semaines)
        {
            if (_semaines == null || _semaines.Count <= 0)
            {
                this.ShowPopup(PopupValues.BindingFail);
                return;
            }
                

            lvWeek.ItemsSource = _semaines.OrderByDescending(semaine => Convert.ToInt32( semaine.Nom.Replace("S",String.Empty)));
            lvWeek.Visibility = Visibility.Visible;
        }

        private void lvWeek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvWeek.SelectedValue?.GetType() == typeof(Semaine))
            {

                try
                {
                    Semaine week = (Semaine)lvWeek.SelectedValue;
                    List<Semaine> semaines = new List<Semaine>() { week };
                    semaines.UpdateJson("semaine.json");
                    btnBack_Click(this, new RoutedEventArgs());
                    this.ShowPopup(PopupValues.EnregistrerSucces);
                }
                catch
                {
                    this.ShowPopup(PopupValues.EnregistrerFail);
                }

         
            }else
                this.ShowPopup(PopupValues.BindingFail);
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

        private void panExcelSemaine_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                IsDropAllowed(files, bordExcelSemaine, lbDropExcelSemaine);

            }


        }
        private void panExcelSemaine_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileInfo? fileInfo = IsDropAllowed(files, bordExcelSemaine, lbDropExcelSemaine);
                if (fileInfo != null)
                {
                    ExcelManager excel = new ExcelManager(fileInfo);
                    ExcelWeekParser excelWeekParser = new ExcelWeekParser(excel.ShowCellsValues());
                    this._excelWeekParser = excelWeekParser;
                    Semaines = excelWeekParser.GetWeeks();
                    BindWeekListView(Semaines);
                }

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }
    }
}
