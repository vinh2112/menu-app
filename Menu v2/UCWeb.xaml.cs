
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCWeb.xaml
    /// </summary>
    public partial class UCWeb : UserControl
    {
        public UCWeb()
        {
            InitializeComponent();
        }

        private void Facebook_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.facebook.com/");
        }

        private void Youtube_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.youtube.com/");
        }

        private void Rain_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://rainymood.com/");
        }

        private void Fanpage_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.facebook.com/groups/mixigaming");
        }

        private void YoutubeMixi_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.youtube.com/c/MixiGamingofficial");
        }

        private void NimoTV_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.nimo.tv/mixi");
        }
    }
}
