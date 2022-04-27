using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCWindowApp.xaml
    /// </summary>
    public partial class UCWindowApp : UserControl
    {
        public UCWindowApp()
        {
            InitializeComponent();
        }

        private void ThisPC_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "shell:MyComputerFolder");
        }

        private void ControlPanel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("control");
        }

        private void RecycleBin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "shell:RecycleBinFolder");
        }
        private void DiskCleanup_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cleanmgr");
        }

        private void Spotify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("spotify");
            }
            catch { }
        }

        private void Photoshop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("photoshop");
            }
            catch { }
        }
    }
}
