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
using Json;
using Utils;
using Popups;
using System.IO;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Flow.Model;
using MailCreator.App_Code.Utils;
using Telerik.Windows.Controls;
using MailCreator.App_Code.DataView;

namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailSetup.xaml
    /// </summary>
    public partial class MailSetupWindow : UserControl
    {
        private EmailGeneric email = null;
        public List<string> MatchTextStrings = new List<string>();

        public MailSetupWindow()
        {
            InitializeComponent();
            BindDataMailPanel();
        }

        private void BindDataMailPanel()
        {

            try

            {
            
                List<PropertyInfo> propertyInfos = typeof(Relance).GetProperties().ToList();

                propertyInfos.ForEach(propertyInfo =>
                {

                    if(propertyInfo.PropertyType == typeof(EmailProperty))
                    {

                        PropertyData propertyData = PropertyDatas.GetPropertyDataByName(propertyInfo.Name);
                        if(propertyData != null)
                        {
                            ucMailData ucMailData = new ucMailData();
                            MatchTextStrings.Add(propertyData.MatchText);
                            ucMailData.MatchText =  propertyData.MatchText;
                            ucMailData.TextValue = propertyData.Description;
                            spMailData.Children.Add(ucMailData);
                        }
                    
                    }

                });


                List<EmailGeneric> emails = JsonFileUtils.Read<List<EmailGeneric>>("emailGenerics.json");
                if(emails != null && emails.Count > 0)
                {
                    email = emails.FirstOrDefault();
                    BindEmail();
                }

            }
            catch
            {
                this.ShowPopup(PopupValues.BindingFail);
            }




        }

        private void BindEmail()
        {
            try
            {


                rtbMailObjet.Document = TelerikExtensions.HtmlToRtb(email.Sujet);
                rtbMailBodyHeader.Document =  TelerikExtensions.HtmlToRtb(email.Body.GetHeader());
                rtbMailBody.Document =  TelerikExtensions.HtmlToRtb(email.Body.GetBody());
                rtbMailBodyFooter.Document =  TelerikExtensions.HtmlToRtb(email.Body.GetFooter());

            }
            catch
            {

                this.ShowPopup(PopupValues.BindingFail);
            }

        }

        private void ChangeTextColor(RichTextBox rtb, string[] textsToFind, SolidColorBrush color)
        {

            FlowDocument document = rtb.Document;

            if (document == null) return;

            foreach (string textToFind in textsToFind)
            {
                TextPointer start = rtb.Document.ContentStart;

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




        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

            try
            {
             



                new List<EmailGeneric>() { new EmailGeneric(TelerikExtensions.RtbToHtml(rtbMailObjet), new MailBody(TelerikExtensions.RtbToHtml(rtbMailBodyHeader), TelerikExtensions.RtbToHtml(rtbMailBody), TelerikExtensions.RtbToHtml(rtbMailBodyFooter)))}.UpdateJson<EmailGeneric>("emailGenerics.json");
                this.ShowPopup(PopupValues.ModificationSucces);
            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }

      
        }

    }
}
