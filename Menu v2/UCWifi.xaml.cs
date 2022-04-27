using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCWifi.xaml
    /// </summary>
    public partial class UCWifi : UserControl
    {
        public UCWifi(string name, string pass)
        {
            InitializeComponent();
            lbNameWifi.Text = name;
            lbPassword.Text = pass;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(lbPassword.Text);
            Tool.Instance.ShowCopiedWifi();
        }
    }
}
