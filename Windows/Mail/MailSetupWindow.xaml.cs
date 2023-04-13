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

namespace MailCreator.Windows.Mail
{
    /// <summary>
    /// Logique d'interaction pour MailSetup.xaml
    /// </summary>
    public partial class MailSetupWindow : UserControl
    {
        public MailSetupWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Content = homeWindow;
        }

        private void ChangeTextColor(string[] textsToFind, SolidColorBrush color)
        {
            //if (fdMailBody == null || pgMailBody == null) return;

            FlowDocument document = rtbMailBody.Document;

            // Vérifier si le FlowDocument est nul
            if (document == null) return;

            // Récupérer la liste des paragraphes
            List<Paragraph> paragraphs = new List<Paragraph>();
            document.Blocks.ToList().ForEach(b =>
            {
                if (b is Paragraph)
                    paragraphs.Add((Paragraph)b);
            });

            foreach (Paragraph paragraph in paragraphs)
            {
                paragraph.LineHeight = 7;
                paragraph.Inlines.ToList().ForEach(inline =>
                {
                    if (inline.GetType() == typeof(Run))
                    {



                        Run run = (Run)inline;
                     
                        foreach (string textToFind in textsToFind)
                        {



                            if (run.Text.Contains(textToFind))
                            {

                                if (run.Text == textToFind)
                                {
                                    run.Foreground = color;
                                    return;
                                }

                                int initialRunId = paragraph.Inlines.ToList().IndexOf(inline);

                                List<Run> runList = new List<Run>();

                                string[] textSplited = run.Text.Split(new string[] { textToFind }, StringSplitOptions.RemoveEmptyEntries);

                                if (textSplited.Length > 0)
                                {

                                    for (int i = 0; i < textSplited.Length; i++)
                                    {
                                        runList.Add(new Run(textSplited[i]));
                                        if (i != textSplited.Length - 1 || textSplited.Length == 1)
                                        {
                                            Run runTextFind = new Run(textToFind);
                                            runTextFind.Foreground = color;
                                            runList.Add(runTextFind);
                                        }
                                    }
                                    TextPointer pointer = rtbMailBody.Document.ContentEnd;
                                    runList.ForEach(Localrun =>
                                    {
                                        paragraph.Inlines.InsertBefore(inline, Localrun);
                                        pointer = Localrun.ContentEnd;
                                    });
                                    paragraph.Inlines.Remove(inline);

                                    pointer = rtbMailBody.Document.ContentEnd;
                                }



                            }
                            else
                                run.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                });

            }





        }

        private void RichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ChangeTextColor(new string[]
            {
                "$nomMatiere$",
                "$nomEnseignant$",
            }, new SolidColorBrush(Colors.ForestGreen));

        }
    }
}
