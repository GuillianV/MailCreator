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
    /// Logique d'interaction pour ProfesseurCreateWindow.xaml
    /// </summary>
    public partial class ProfesseurCreateWindow : UserControl
    {
        public List<Professeur> Professeurs { get; set; }
        int _selectedIndex = -1;

        public ProfesseurCreateWindow(List<Professeur> professeurs)
        {
            InitializeComponent();

            Professeurs = professeurs != null && professeurs.Count > 0 ? professeurs : new List<Professeur>();
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

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            Professeur professeur = new Professeur(txtCivilite.Text.MatchMailtypeText(), txtNom.Text.MatchMailtypeText(), txtPrenom.Text.MatchMailtypeText(), txtMail.Text.MatchMailtypeText());
            if (_selectedIndex == -1)
            {
                Professeurs.Add(professeur);
                _selectedIndex = Professeurs.Count -1;
            }
            else
                Professeurs[_selectedIndex] = professeur;

            JsonFileUtils.Write(Professeurs, typeof(List<Professeur>), "professeurs.json");
            Professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
        }
    }
}
