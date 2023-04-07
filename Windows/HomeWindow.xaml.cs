using DataView;
using Excel;
using ExcelPart;
using Json;
using MailCreator.Windows.Professeurs;
using MailCreator.Windows.Week;
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


namespace MailCreator.Windows
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : UserControl
    {

        public HomeWindow()
        {
            InitializeComponent();
            BindSemaineEdit();
        }


        private void btnEnsignants_Click(object sender, RoutedEventArgs e)
        {
            ProfesseurWindow professeurWindow = new ProfesseurWindow();
            this.Content = professeurWindow;
        }
        private void btnSemaineAdd_Click(object sender, RoutedEventArgs e)
        {
            WeekSelectWindow weekSelectWindow = new WeekSelectWindow();
            this.Content = weekSelectWindow;
        }

        public void BindSemaineEdit()
        {
            List<Semaine> semaines = JsonFileUtils.Read<List<Semaine>>("semaine.json");
            Semaine semaine = semaines.FirstOrDefault();
            if (semaine != null)
            {
                lbSemaineEdit.Content = $"Modifier la semaine {semaine.Nom}";
            }
            else
            {

            }

        }

        private void btnSemaineEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMail_Click(object sender, RoutedEventArgs e)
        {

        }


    }

}
