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
using System.Windows.Controls.Primitives;
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
    /// Logique d'interaction pour ProfesseurUpdateWindow.xaml
    /// </summary>
    public partial class ProfesseurUpdateWindow : UserControl
    {
        public List<Professeur> Professeurs  { get; set; }

        int _selectedIndex = -1;

        public ProfesseurUpdateWindow(List<Professeur> professeurs, int selectedIndex)
        {
            InitializeComponent();

            Professeurs = professeurs != null && professeurs.Count > 0 ? professeurs : new List<Professeur>();
            _selectedIndex =  Professeurs.Count > selectedIndex ? selectedIndex : -1;

            BindModification();
            BindGrid();
        }

    

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedIndex >= 0 && Professeurs.Count > 0)
            {

                Professeur professeur = new Professeur(txtCivilite.Text.MatchMailtypeText(),txtNom.Text.MatchMailtypeText(),txtPrenom.Text.MatchMailtypeText(),txtMail.Text.MatchMailtypeText());
                Professeurs[_selectedIndex] = professeur;
                Professeurs = Professeurs.UpdateJsonProfesseurs();
                BindGrid();
                BindModification();
            }
          
        }



        private void lvProfesseurs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object objet = lvProfesseurs.SelectedValue;
            if (objet != null && objet.GetType() == typeof(Professeur))
            {
                Professeur professeur = (Professeur)objet;
                _selectedIndex = Professeurs.IndexOf(professeur);
                BindModification();
            }
        }

        private void BindModification()
        {
            if (Professeurs == null || _selectedIndex < 0)
                return;

            Professeur professeurCible = Professeurs[_selectedIndex];
            txtCivilite.Text = professeurCible.Civilite;
            txtMail.Text = professeurCible.Mail;
            txtNom.Text = professeurCible.Name;
            txtPrenom.Text = professeurCible.Prenom;

        }

        private void BindGrid()
        {

            lvProfesseurs.ItemsSource = Professeurs;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ProfesseurWindow professeurWindow = new ProfesseurWindow();
            this.Content = professeurWindow;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

       
        private void btnPop_Click(object sender, RoutedEventArgs e)
        {
            this.ShowPopup(PopupValues.EnregistrerFail);
        }
    }
}
