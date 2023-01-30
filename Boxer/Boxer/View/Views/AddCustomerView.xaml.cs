using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Boxer.View.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddCustomerView.xaml
    /// </summary>
    public partial class AddCustomerView : UserControl
    {
        public AddCustomerView()
        {
            InitializeComponent();
        }

        private static readonly Regex _phoneRegex = new Regex("[^0-9+]+");
        private void Phone_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsPhoneTextAllowed(e.Text);
        }
        private void Phone_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsPhoneTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
        private static bool IsPhoneTextAllowed(string text)
        {
            return !_phoneRegex.IsMatch(text);
        }
    }
}
