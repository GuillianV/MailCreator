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
    /// Logique d'interaction pour WeekUpdateWindow.xaml
    /// </summary>
    public partial class WeekUpdateWindow : UserControl
    {
        public List<Matiere> Matieres {  get; set; }
        public List<Professeur> Professeurs { get; set; }

        public DateTime DateTimeDebutSemaine { get; set; }
        public Semaine Semaine { get; set; }

        public WeekUpdateWindow()
        {
            

            InitializeComponent();
            BindGridMatiere();
            BindDatePicker();


        }

        public void BindGridMatiere()
        {
            try
            {
                List<Semaine> semaines = JsonFileUtils.Read<List<Semaine>>("semaine.json");
                Professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
                Semaine = semaines.FirstOrDefault();
                Matieres = Semaine.Matieres;
                dgMatieres.ItemsSource = Matieres;
                

            }
            catch(Exception ex)
            {

                this.ShowPopup(PopupValues.BindingFail);
            }

       

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void dgMatieres_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {


                if (Semaine == null)
                {
                    this.ShowPopup(PopupValues.ModificationFail);
                    return;
                }


                new List<Semaine>() { Semaine }.UpdateJson<Semaine>("semaine.json");
                this.ShowPopup(PopupValues.ModificationSucces);

            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }

        }

        private void BindDatePicker()
        {
            try
            {
                if (Semaine == null)
                {
                    this.ShowPopup(PopupValues.BindingFail);
                    return;
                }
                DateTimeDebutSemaine = Semaine.Dates != null && Semaine.Dates.Count > 0 ? Semaine.Dates.First() : DateTime.Now;
                DataContext = this;
            }
            catch
            {
                this.ShowPopup(PopupValues.BindingFail);
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (Semaine != null && dpSemaine.SelectedDate != null)
                {
                    List<DateTime> SavedDates = Semaine.Dates;
                    List<DateTime> localDates = new List<DateTime>() { dpSemaine.SelectedDate.Value, dpSemaine.SelectedDate.Value.AddDays(1), dpSemaine.SelectedDate.Value.AddDays(2), dpSemaine.SelectedDate.Value.AddDays(3), dpSemaine.SelectedDate.Value.AddDays(4) };
                   
                    if(SavedDates == null || SavedDates.Count <= 0 || SavedDates.FirstOrDefault().Day != localDates.FirstOrDefault().Day)
                    {
                        Semaine.Dates = localDates;
                        new List<Semaine>() { Semaine }.UpdateJson<Semaine>("semaine.json");
                        this.ShowPopup(PopupValues.ModificationSucces);
                    }
              
                }

            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }
        }
    }
}
