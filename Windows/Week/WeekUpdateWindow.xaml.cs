using DataView;
using DataView.Entries;
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

                List<Semaine> semaines = JsonFileUtils.Read<List<Semaine>>("semaine.json");
                if (semaines == null || semaines.Count <= 0 || semaines.First().Matieres == null)
                {
                    this.ShowPopup(PopupValues.BindingFail);
                    return;

                }
                Semaine = semaines.First();


                List<Professeur> professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
                if (professeurs == null || professeurs.Count <= 0)
                {
                    this.ShowPopup(PopupValues.BindingFail);
                    return;

                }


                List<Relance> relances = JsonFileUtils.Read<List<Relance>>("relances.json");
                if (relances == null || relances.Count <= 0)
                {

                    relances = new List<Relance>();

                    semaines.FirstOrDefault().Matieres.ForEach(matiereCible =>
                    {
                        Professeur professeurCible = professeurs.FirstOrDefault(prof =>
                        {
                            if (matiereCible.Enseignant.ToLower().Contains(prof.Nom.ToLower()) && matiereCible.Enseignant.ToLower().Contains(prof.Prenom.ToLower()))
                                return true;

                            return false;

                        });

                        relances.Add(new Relance(
                            professeurCible != null,
                            matiereCible.Visioconference,
                            new EmailProperty("Visioconference", "$visioconference$", matiereCible.Visioconference ? "Ce cours sera en visioconférence." : ""),
                            new EmailProperty("Promo", "$promo$", matiereCible.Promo),
                            new EmailProperty("Nom de la matière", "$nomMatiere$", matiereCible.Nom),
                            new EmailProperty("Salle disponibles", "$salles$", matiereCible.Salle),
                            new EmailProperty("Jour", "$jour$", matiereCible.Jour),
                            new EmailProperty("Date de la séance", "$date$", dpSemaine.SelectedDate != null ? dpSemaine.SelectedDate.ToString() : DateTimeDebutSemaine.ToString()),
                            new EmailProperty("Seance", "$seance$", matiereCible.Seance),
                            new EmailProperty("Civilité de l'enseignant", "$enseignantCivilite$", professeurCible?.Civilite),
                            new EmailProperty("Nom de l'enseignant", "$enseignantNom$", professeurCible?.Nom),
                            new EmailProperty("Prenom de l'enseignant", "$enseignantPrenom$", professeurCible?.Prenom),
                            new EmailProperty("Mail de l'enseignant", "$enseignantMail$", professeurCible?.Mail)
                            ));

                    });
                    relances.UpdateJson<Relance>("relances.json");
                }
                Relances = relances;
                dgMatieres.ItemsSource = Relances;

            }
            catch (Exception ex)
            {

                this.ShowPopup(PopupValues.BindingFail);
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

                    if (SavedDates == null || SavedDates.Count <= 0 || SavedDates.FirstOrDefault().Day != localDates.FirstOrDefault().Day)
                    {
                        Semaine.Dates = localDates;
                        new List<Semaine>() { Semaine }.UpdateJson<Semaine>("semaine.json");
                                               
                    }

                }

            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Relances == null)
                {
                    this.ShowPopup(PopupValues.ModificationFail);
                    return;
                }

                Relances.ForEach(relance =>
                {
                    relance.Visioconference = new EmailProperty("Visioconference", "$visioconference$", relance.EstVisioconference ? "Ce cours sera en visioconférence." : "");
                    int addDays = 0;
                    if (!string.IsNullOrEmpty(relance.Jour.Traduction))
                    {
                        Jour jCible = Jours.GetJours().FirstOrDefault(jour => jour.Nom == relance.Jour.Traduction);
                        if (jCible != null && dpSemaine.SelectedDate != null)
                        {
                            relance.Date.Traduction = dpSemaine.SelectedDate.Value.AddDays(jCible.AddDays).ToString("dd/MM/yyyy");
                        }
                    }
                });
                Relances = Relances.UpdateJson<Relance>("relances.json");
                dgMatieres.ItemsSource = Relances;
                this.ShowPopup(PopupValues.ModificationSucces);
                return;

            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }

        }
    }
}
