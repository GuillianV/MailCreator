using DataView;
using Office.DataView;
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
using Office;
using Json;
using System.Reflection;
using DataView.Entries;
using Outlook = Microsoft.Office.Interop.Outlook;
using Utils;
using Popups;
using MailCreator.App_Code.DataView;

namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailDraftWindow.xaml
    /// </summary>
    public partial class MailDraftWindow : UserControl
    {

        public List<MailData> Mails { get; set; }

        private EmailGeneric EmailGeneric = null;

        private List<Relance> Relances = null;

        public MailDraftWindow()
        {
            InitializeComponent();

            BindData();
            BindMails();
            BindOffice();
        }

        private void BindOffice()
        {
            if (!OfficeUtils.Authenticate())
            {
                DisableBtns();
                this.ShowPopup(PopupValues.ConnexionOutlookFail);
                return;
            }


            foreach (Outlook.Account account in OfficeUtils.accounts)
            {
                cbAccounts.Items.Add(account.SmtpAddress);
            }
            cbAccounts.SelectedValue = OfficeUtils.account.SmtpAddress;


        }

        private void DisableBtns()
        {
            cbAccounts.IsEnabled = false;
            btnCreer.IsEnabled = false;
            btnSupprimer.IsEnabled = false;
            btnModifier.IsEnabled = false;
            btnEnvoyer.IsEnabled = false;
        }

        private void BindData()
        {
            List<EmailGeneric> emailGenerics = JsonFileUtils.Read<List<EmailGeneric>>("emailGenerics.json");
            if (emailGenerics != null && emailGenerics.Count > 0)
                EmailGeneric = emailGenerics.FirstOrDefault();

            List<Relance> relances = JsonFileUtils.Read<List<Relance>>("relances.json");
            if (relances != null && relances.Count > 0)
                Relances = relances.Where(relance => relance.EstRelancable).ToList();

            Mails = new List<MailData>();

            if (EmailGeneric == null || Relances == null)
            {
                DisableBtns();
                return;
            }


        }

        private void BindMails()
        {
            Mails.Clear();
            OfficeUtils.GetAllDrafts().ForEach(mailItem =>
            {
                Mails.Add(new MailData(mailItem.To, mailItem.Subject, mailItem.Body));
            });
            lvMails.ItemsSource = Mails;
            lvMails.Items.Refresh();
            cbAccounts.IsEnabled = Mails.Count <= 0;
            btnSupprimer.IsEnabled = Mails.Count > 0;
            btnModifier.IsEnabled = Mails.Count > 0;
            btnEnvoyer.IsEnabled = Mails.Count > 0;

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<PropertyInfo> propertyInfos = typeof(Relance).GetProperties().ToList();
                OfficeUtils.GetAllDrafts().ForEach(draft =>
                {

                    draft.Delete();

                });
                OfficeUtils.GetAllDrafts().Clear();
             
                List<List<Relance>> relanceGroupedMail =  Relances.GroupBy(group =>  group.EnseignantMail.Traduction).Select(grp => grp.ToList()).ToList();
                if(relanceGroupedMail.Count <= 0)
                {
                    this.ShowPopup(PopupValues.CreationFail);
                    return;
                }

                relanceGroupedMail.ForEach(relances =>
                {
                    if (relances.Count > 0)
                    {

                        string To = "";
                        string Subject = EmailGeneric.Sujet;
                        string Header = EmailGeneric.Body.GetHeader();
                        string Body = EmailGeneric.Body.GetBody();
                        string Footer = EmailGeneric.Body.GetFooter();
                        string FullBody = "";

                        relances.ForEach(relance =>
                        {

                            string BodyPart = Body;
                            propertyInfos.ForEach(propertyInfo =>
                            {
                                if (propertyInfo.PropertyType == typeof(EmailProperty))
                                {

                                    EmailProperty emailProperty = (EmailProperty)propertyInfo.GetValue(relance);
                                    if (emailProperty.PropertyData.Nom == PropertyDatas.EnseignantMail.Nom)
                                    {
                                        To = emailProperty.Traduction;
                                    }

                                    Subject = Subject.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);
                                    Header = Header.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);
                                    Footer = Footer.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);

                                    BodyPart = BodyPart.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);
                                }

                            });

                            FullBody += BodyPart;
                        });
                        MailBody mailBody = new MailBody(Header, FullBody, Footer);
                        OfficeUtils.CreateDraft(new MailData(To, Subject, mailBody.GetRowText()));

                    }

                });


                BindMails();
                this.ShowPopup(PopupValues.CreationSucces);
            }
            catch
            {
                this.ShowPopup(PopupValues.CreationFail);
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lvMails.SelectedValue != null && lvMails.SelectedValue.GetType() == typeof(MailData))
                {

                    MailData mailData = (MailData)lvMails.SelectedValue;

                    Outlook.MailItem mailItem = OfficeUtils.GetAllDrafts().FirstOrDefault(md => md.To == mailData.Destinataire && md.Subject == mailData.Objet && md.Body == mailData.Body);
                    if (mailItem != null)
                    {
                        if (OfficeUtils.MailExist(mailItem))
                            mailItem.Delete();

                    }

                    Mails.Remove(mailData);
                    BindMails();
                    this.ShowPopup(PopupValues.SuppressionSucces);
                    return;

                }
                this.ShowPopup(PopupValues.SupprimerFail);
            }
            catch
            {
                this.ShowPopup(PopupValues.SupprimerFail);
            }



        }


        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lvMails.SelectedValue != null && lvMails.SelectedValue.GetType() == typeof(MailData))
                {

                    MailData mailData = (MailData)lvMails.SelectedValue;

                    Outlook.MailItem mailItem = OfficeUtils.GetAllDrafts().FirstOrDefault(md => md.To == mailData.Destinataire && md.Subject == mailData.Objet && md.Body == mailData.Body);
                    if (mailItem != null)
                    {

                        MailUpdateWindow mailUpdateWindow = new MailUpdateWindow(mailItem.EntryID);
                        this.Content = mailUpdateWindow;
                        return;
                    }
                    else
                    {
                        Mails.Remove(mailData);
                        BindMails();
                    }



                }
                this.ShowPopup(PopupValues.ModificationFail);
            }
            catch
            {
                this.ShowPopup(PopupValues.ModificationFail);
            }


        }

        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OfficeUtils.SendDrafts();
                this.ShowPopup(PopupValues.EnvoiSucces);
            }
            catch
            {
                this.ShowPopup(PopupValues.EnvoiFail);
            }


        }

        private void cbAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Outlook.Account account in OfficeUtils.accounts)
            {
                if (cbAccounts.SelectedValue.ToString() == account.SmtpAddress)
                {

                    OfficeUtils.account = account;

                }
            }
        }
    }
}
