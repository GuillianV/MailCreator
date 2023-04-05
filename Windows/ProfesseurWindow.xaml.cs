using Json;
using JsonPart;
using JsonPart.Records;
using Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace MailCreator.Windows
{
    /// <summary>
    /// Logique d'interaction pour ProfesseurWindow.xaml
    /// </summary>
    public partial class ProfesseurWindow : UserControl
    {
        public List<Professeur> Professeurs  { get; set; }


        public ProfesseurWindow()
        {
            InitializeComponent();
            BindGrid();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }
     

        private void BindGrid()
        {
            try
            {
                Professeurs =  JsonFileUtils.Read<List<Professeur>>("professeurs.json");
                lvProfesseurs.ItemsSource = Professeurs;
            }catch
            {
                this.ShowPopup(PopupValues.BindingFail);
            }

        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            ProfesseurCreateWindow professeurCreateWindow = new ProfesseurCreateWindow(Professeurs);
            this.Content = professeurCreateWindow;
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            object objet = lvProfesseurs.SelectedValue;
            if (objet != null && objet.GetType() == typeof(Professeur))
            {
                Professeur professeur = (Professeur)objet;
                ProfesseurUpdateWindow professeurUpdateWindow = new ProfesseurUpdateWindow(Professeurs, Professeurs.IndexOf(professeur));
                this.Content = professeurUpdateWindow;
            }
            else
            {
                this.ShowPopup(PopupValues.MissingSelectValueWarn);
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            object objet = lvProfesseurs.SelectedValue;
            if (objet != null && objet.GetType() == typeof(Professeur))
            {

                try
                {
                    Professeur professeur = (Professeur)objet;
                    Professeurs.Remove(professeur);
                    Professeurs.UpdateJsonProfesseurs();
                    BindGrid();
                }
                catch
                {
                    this.ShowPopup(PopupValues.SupprimerFail);
                }


            }
        }

        private void lvProfesseurs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ShowTips();

        }

        private async void ShowTips()
        {

            btnModifier.BorderThickness = new Thickness(2);
            btnSupprimer.BorderThickness = new Thickness(2);
            await Task.Delay(2000);
            btnModifier.BorderThickness = new Thickness(1);
            btnSupprimer.BorderThickness = new Thickness(1);
        }

    }
}
