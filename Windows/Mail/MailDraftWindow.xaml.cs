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

namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailDraftWindow.xaml
    /// </summary>
    public partial class MailDraftWindow : UserControl
    {

        public List<MailData> Mails = new List<MailData>();

        private EmailGeneric EmailGeneric = null;

        private List<Relance> Relances = null;

        public MailDraftWindow()
        {
            InitializeComponent();
            BindData();
            BindMails();
        }

        private void BindData()
        {
            List<EmailGeneric> emailGenerics = JsonFileUtils.Read<List<EmailGeneric>>("emailGenerics.json");
            if(emailGenerics != null && emailGenerics.Count > 0)
                EmailGeneric = emailGenerics.FirstOrDefault();

            List<Relance> relances = JsonFileUtils.Read<List<Relance>>("relances.json");
            if (relances != null && relances.Count > 0)
                Relances = relances.Where(relance => relance.EstRelancable).ToList();


            if (EmailGeneric == null || Relances == null)
            {
                btnCreer.IsEnabled = false;
                btnSupprimer.IsEnabled = false;
                btnModifier.IsEnabled = false;
                btnEnvoyer.IsEnabled = false;
                return;
            }


        }

        private void BindMails()
        {
            OfficeUtils.mailDrafts.ForEach(mailItem =>
            {
                Mails.Add(new MailData(mailItem.To, mailItem.Subject, mailItem.Body));
            });
            lvMails.ItemsSource = Mails;
            DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {

            List<PropertyInfo> propertyInfos = typeof(Relance).GetProperties().ToList();
            Mails.Clear();
            Relances.ForEach(relance =>
            {
                string To = "";
                string Subject = EmailGeneric.Sujet;
                string Body = EmailGeneric.Body;

               
                propertyInfos.ForEach(propertyInfo =>
                {
                    if (propertyInfo.PropertyType == typeof(EmailProperty))
                    {

                        EmailProperty emailProperty = (EmailProperty)propertyInfo.GetValue(relance);
                        if(emailProperty.PropertyData.Nom == PropertyDatas.EnseignantMail.Nom)
                        {
                            To = emailProperty.Traduction;
                        }

                        Subject.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);
                        Body.Replace(emailProperty.PropertyData.MatchText, emailProperty.Traduction);


                    }

                });

                Mails.Add(new MailData(To, Subject, Body));


            });

            lvMails.ItemsSource = Mails;
            DataContext = this;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
