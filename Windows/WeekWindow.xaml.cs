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

        private List<WeekViewBinding> Weeks = new List<WeekViewBinding>();

        public WeekWindow(ExcelWeekParser excelWeekParser)
        {


            InitializeComponent();

            Weeks = excelWeekParser.GetWeeks();
            Matieres = excelWeekParser.GetWeek("S49").Matieres; //Weeks.OrderBy(w => Convert.ToInt32(w.Semaine.Replace("S", String.Empty))).ToList();
            DataContext = this;

        }
    }
}
