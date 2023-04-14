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

namespace MailCreator.UserControls
{

    public partial class ucMailData : UserControl
    {
        public static readonly DependencyProperty MatchTextProperty = DependencyProperty.Register(
       "MatchText", typeof(string), typeof(ucMailData), new PropertyMetadata(null, OnMatchTextChanged));

        public string MatchText
        {
            get { return (string)GetValue(MatchTextProperty); }
            set { SetValue(MatchTextProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register(
        "TextValue", typeof(string), typeof(ucMailData), new PropertyMetadata(null, OnMatchTextChanged));

        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }


        private static void OnMatchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucMailData control = d as ucMailData;
            control.BindText();
        }


        public ucMailData()
        {
            InitializeComponent();
            BindText();
        }

        private void BindText()
        {
            tbName.Text = MatchText;
            bdNomMatiere.ToolTip = TextValue ?? "";
        }
    }
}
