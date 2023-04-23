using Office;
using Office.DataView;
using Popups;
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
using Utils;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailUpdateWindow.xaml
    /// </summary>
    public partial class MailUpdateWindow : UserControl
    {

        string EntryId;
        Outlook.MailItem mailItem;

        public MailUpdateWindow(string mailEntryId)
        {
            InitializeComponent();
            this.EntryId = mailEntryId;
            BindMail();
        }

        private void BindMail()
        {
            mailItem = OfficeUtils.GetAllDrafts().FirstOrDefault(mi => mi.EntryID == EntryId);
            if(mailItem == null)
            {
                btnBack_Click(this, new RoutedEventArgs());
                return;
            }


            mailItem.Subject.Split(new string[] { "\\r\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList()?.ForEach(txt =>
            {
                rtbMailObjet.Document.Blocks.Add(new Paragraph(new Run(txt)));
            });

            mailItem.Body.Split(new string[] { "\\r\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList()?.ForEach(txt =>
            {
                rtbMailBody.Document.Blocks.Add(new Paragraph(new Run(txt)));
            });

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MailDraftWindow draftWindow = new MailDraftWindow();
            this.Content = draftWindow;
        }


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string subjectText = "";
                TextRange subjectTextRange = new TextRange(
                     rtbMailObjet.Document.ContentStart,
                     rtbMailObjet.Document.ContentEnd
                 );
                subjectText = subjectTextRange.Text;


                string bodyText = "";
                TextRange bodyTextRange = new TextRange(
                     rtbMailBody.Document.ContentStart,
                     rtbMailBody.Document.ContentEnd
                 );
                bodyText = bodyTextRange.Text;
                mailItem.UpdateDraft(new MailData(mailItem.To, subjectText, bodyText));
                this.ShowPopup(PopupValues.ModificationSucces);

            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }
        }
    }
}
