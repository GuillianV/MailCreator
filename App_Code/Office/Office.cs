using Office.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Office
{



    public static class OfficeUtils
    {

        public static List<Outlook.MailItem> mailDrafts = new List<Outlook.MailItem>();  


        public static void Authenticate()
        {
            try
            {

                Outlook.Application outlookApp = new Outlook.Application();

            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static Outlook.MailItem? CreateDraft(MailData mailData) 
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = mailData.Destinataire;
                mailItem.Subject = mailData.Objet;
                mailItem.Body = mailData.Body;
                mailItem.Save();
                mailDrafts.Add(mailItem);
                return mailItem;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public static bool SendDrafts()
        {
            try
            {
                mailDrafts.ForEach(mailDraft => {

                    mailDraft.Send();

                });
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

          
        }



    }
}
