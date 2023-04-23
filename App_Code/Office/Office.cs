using Office.DataView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Office
{



    public static class OfficeUtils
    {

        private static List<Outlook.MailItem> mailDrafts = new List<Outlook.MailItem>();  

        public static Outlook.Account account { get; set; }
        public static Outlook.Accounts accounts { get; set; }
        public static bool Authenticate()
        {
            Outlook.NameSpace ns = null;
            Outlook.MAPIFolder folder = null;
            try
            {

                if (account != null)
                    return true;

                Outlook.Application outlookApp = new Outlook.Application();
                ns = outlookApp.GetNamespace("MAPI");
                folder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                accounts = ns.Accounts;
                if (accounts.Count == 1)
                {
                    var acc = accounts[0];
                    account = acc;
                }
                else if(accounts.Count > 1)
                    account = GetActiveAccount();

                if (account == null || account.SmtpAddress == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                if (folder != null) Marshal.ReleaseComObject(folder);
                if (ns != null) Marshal.ReleaseComObject(ns);
               
            }
        }

        public static bool MailExist(Outlook.MailItem Email)
        {


            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
            Outlook.MailItem mailItem = null;
            try
            {
                mailItem = outlookNamespace.GetItemFromID(Email.EntryID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Outlook.MailItem> GetAllDrafts()
        {
            List<Outlook.MailItem> newDraft = new List<Outlook.MailItem>();
            foreach (Outlook.MailItem mailItemLocal in mailDrafts)
            {
                if (OfficeUtils.MailExist(mailItemLocal))
                {
                    newDraft.Add(mailItemLocal);
                }

            }
            mailDrafts = newDraft;
            return mailDrafts;
        }

        public static Outlook.Account GetActiveAccount()
        {
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.Account account = null;

            // Récupérer le compte de l'utilisateur actif
            Outlook.NameSpace ns = outlookApp.Session;
            Outlook.MAPIFolder inboxFolder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Store store = inboxFolder.Store;
            if (store.IsDataFileStore)
            {
                // Le compte est un fichier de données PST
                account = ns.Accounts.Cast<Outlook.Account>().FirstOrDefault(acc => acc.DeliveryStore.StoreID == store.StoreID);
            }
            else
            {
                // Le compte est un compte Exchange
                account = ns.Accounts.Cast<Outlook.Account>().FirstOrDefault(acc => acc.DisplayName == store.DisplayName);
            }

            if (account != null)
            {
                Console.WriteLine("Le compte de l'utilisateur actif est : " + account.DisplayName);
            }
            else
            {
                Console.WriteLine("Impossible de déterminer le compte de l'utilisateur actif.");
            }


            return account;

        }

        public static Outlook.MailItem? CreateDraft(MailData mailData) 
        {
            try
            {
                if (account == null)
                    return null;

                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.UpdateDraft(mailData);
                mailDrafts.Add(mailItem);
               
                return mailItem;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }


        public static Outlook.MailItem UpdateDraft(this Outlook.MailItem mailItem, MailData mailData)
        {
            try
            {
                if(mailItem != null)
                {
                    mailItem.To = mailData.Destinataire;
                    mailItem.Subject = mailData.Objet;
                    mailItem.Body =  mailData.Body;
                    mailItem.SendUsingAccount = account;
                    mailItem.Save();
                }
                return mailItem;
            }
            catch
            {
                return mailItem;
            }
        }


        public static bool SendDrafts()
        {
            try
            {
                GetAllDrafts()?.ForEach(mailDraft => {

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
