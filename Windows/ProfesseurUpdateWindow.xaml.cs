using Json;
using JsonPart.Records;
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
    /// Logique d'interaction pour ProfesseurUpdateWindow.xaml
    /// </summary>
    public partial class ProfesseurUpdateWindow : UserControl
    {
        Professeur _professeurCible = null;
        public List<Professeur> Professeurs  { get; set; }

        int _selectedIndex;

        public ProfesseurUpdateWindow(List<Professeur> professeurs, int selectedIndex)
        {
            InitializeComponent();

            Professeurs = professeurs != null && professeurs.Count > 0 ? professeurs : new List<Professeur>();
            if (Professeurs.Count > 0)
                _professeurCible = Professeurs.Count > selectedIndex ? Professeurs[selectedIndex] : null;

            _selectedIndex = selectedIndex;

            BindModification();
            BindGrid();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedIndex != null && Professeurs.Count > 0)
            {

                Professeur professeur = new Professeur(txtCivilite.Text.MatchMailtypeText(),txtNom.Text.MatchMailtypeText(),txtPrenom.Text.MatchMailtypeText(),txtMail.Text.MatchMailtypeText());
                Professeurs[_selectedIndex] = professeur;

                JsonFileUtils.Write(Professeurs,typeof(List<Professeur>), "professeurs.json");
                Professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
                _professeurCible =professeur;
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
                _professeurCible = professeur;
                BindModification();
            }
        }

        private void BindModification()
        {
            if (Professeurs == null || _professeurCible == null)
                return;

            txtCivilite.Text = _professeurCible.Civilite;
            txtMail.Text = _professeurCible.Mail;
            txtNom.Text = _professeurCible.Name;
            txtPrenom.Text = _professeurCible.Prenom;

        }

        private void BindGrid()
        {

            lvProfesseurs.ItemsSource = Professeurs;

        }

    }
}
