using ExcelPart;
using ExcelPart.UI;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour WeekWindow.xaml
    /// </summary>
    public partial class WeekWindow : UserControl
    {
        public List<MatiereViewBinding> Matieres { get; set; }
        private List<WeekViewBinding> Weeks { get; set; }

        private ExcelWeekParser _excelWeekParser;

        public WeekWindow(ExcelWeekParser excelWeekParser)
        {
            

            InitializeComponent();

            _excelWeekParser = excelWeekParser;
            Weeks = excelWeekParser.GetWeeks();
            BindWeekListView(Weeks);

        }

        private void btnValiderSemaine_Click(object sender, RoutedEventArgs e)
        {
           
                WeekViewBinding weekViewBinding = _excelWeekParser.GetWeek(txtSemaine.Text);
                BindMatiereListView(weekViewBinding?.Matieres);

        }

        private void BindMatiereListView(List<MatiereViewBinding> _matieres)
        {
            if (_matieres == null || _matieres.Count <= 0)
                return;

            lvMatieres.ItemsSource = _matieres;
            lvWeek.Visibility = Visibility.Hidden;
            lvMatieres.Visibility = Visibility.Visible;
        }

        private void BindWeekListView(List<WeekViewBinding> _semaines)
        {
            if (_semaines == null || _semaines.Count <= 0)
                return;

            lvWeek.ItemsSource = _semaines.OrderByDescending(semaine => Convert.ToInt32( semaine.Semaine.Replace("S",String.Empty)));
            lvWeek.Visibility = Visibility.Visible;
            lvMatieres.Visibility = Visibility.Hidden;
        }

        private void lvWeek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvWeek.SelectedValue?.GetType() == typeof(WeekViewBinding))
            {

                WeekViewBinding week = (WeekViewBinding)lvWeek.SelectedValue;
                txtSemaine.Text = week.Semaine;

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }
    }
}
