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

        public List<Relance> Relances { get; set; }

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

                List<Relance> relances = JsonFileUtils.Read<List<Relance>>("relances.json");

                if (relances == null || relances.Count <= 0)
                {
                    List<Semaine> semaines = JsonFileUtils.Read<List<Semaine>>("semaine.json");
                    List<Professeur> professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
                    relances = new List<Relance>();
                    if (semaines != null &&  semaines.Count > 0 && semaines.FirstOrDefault().Matieres != null && professeurs != null && professeurs.Count > 0)
                    {
                        semaines.FirstOrDefault().Matieres.ForEach(Matiere =>
                        {

                            relances.Add(new Relance(Matiere, professeurs.FirstOrDefault(prof =>
                            {
                                if (Matiere.Enseignant.ToLower().Contains(prof.Nom.ToLower()) && Matiere.Enseignant.ToLower().Contains(prof.Prenom.ToLower()))
                                    return true;

                                return false;

                            })));

                        });
                    }
                 
                }
                Relances = relances;
                dgMatieres.ItemsSource = Relances;

            }
            catch (Exception ex)
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


                if (Relances == null)
                {
                    this.ShowPopup(PopupValues.ModificationFail);
                    return;
                }

                Relances.UpdateJson<Relance>("relances.json");
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
