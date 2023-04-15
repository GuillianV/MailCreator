using DataView;
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
using System.Reflection;
using MailCreator.UserControls;
using DataView.Entries;

namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailSetup.xaml
    /// </summary>
    public partial class MailSetupWindow : UserControl
    {

        public List<string> MatchTextStrings = new List<string>();

        public MailSetupWindow()
        {
            InitializeComponent();
            BindDataMailPanel();
        }

        private void BindDataMailPanel()
        {
            Func<string, string> LowerFirst = (input) => {
                return char.ToLower(input[0]) + input.Substring(1);
            };

            List<PropertyInfo> propertyInfos = typeof(Relance).GetProperties().ToList();

            propertyInfos.ForEach(propertyInfo =>
            {

                if(propertyInfo.PropertyType == typeof(EmailProperty) && PropertyDatas.GetPropertyDataByName(propertyInfo.Name, out PropertyData propertyData))
                {
                    
                    ucMailData ucMailData = new ucMailData();
                    MatchTextStrings.Add(propertyData.MatchText);
                    ucMailData.MatchText =  propertyData.MatchText;
                    ucMailData.TextValue = propertyData.Description;
                    spMailData.Children.Add(ucMailData);
                }

            });

           

        }


        private void ChangeTextColor(string[] textsToFind, SolidColorBrush color)
        {

            FlowDocument document = rtbMailBody.Document;

            if (document == null) return;

            foreach (string textToFind in textsToFind)
            {
                TextPointer start = rtbMailBody.Document.ContentStart;

                while (start != null)
                {
                    if (start.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        string textRun = start.GetTextInRun(LogicalDirection.Forward);

                        // Rechercher la position du texte à surbriller dans le TextPointer actuel
                        int indexInRun = textRun.IndexOf(textToFind);
                        if (indexInRun >= 0)
                        {
                            // Créer le TextPointer de début et de fin de la surbrillance
                            TextPointer startHighlight = start.GetPositionAtOffset(indexInRun);
                            TextPointer endHighlight = startHighlight.GetPositionAtOffset(textToFind.Length);

                            // Appliquer la surbrillance
                            TextRange textRange = new TextRange(startHighlight, endHighlight);
                            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, color);

                            // Trouver la position suivant la fin de la surbrillance
                            TextPointer next = endHighlight.GetNextInsertionPosition(LogicalDirection.Forward);

                            // Si la position suivante n'est pas null et qu'elle n'est pas à la fin du document, enlever la surbrillance
                            if (next != null && next.CompareTo(document.ContentEnd) != 0)
                            {
                                TextRange nextTextRange = new TextRange(endHighlight, next);
                                nextTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
                            }

                            // Continuer la recherche après la fin de la surbrillance
                            start = next;
                            continue;
                        }
                    }

                    start = start.GetNextInsertionPosition(LogicalDirection.Forward);
                }
            }

        }

        private void RichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ChangeTextColor(MatchTextStrings.ToArray(), new SolidColorBrush(Colors.ForestGreen));

        }




        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
