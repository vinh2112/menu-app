using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCOffice.xaml
    /// </summary>
    public partial class UCOffice : UserControl
    {
        public UCOffice()
        {
            InitializeComponent();
        }
        string path = @"C:\Program Files (x86)\Microsoft Office\root\Office16";

        private void btnWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\winword.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void btnPowPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\powerpnt.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\excel.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }

        private void btnAccess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\msaccess.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }

        private void btnOneNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\OneNote 2016.lnk");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }

        private void btnOutLook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\Outlook.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }

        private void btnPublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(path + @"\mspub.exe");
                MainWindow.Instance.WindowState = WindowState.Minimized;
            }
            catch { }
        }
    }
}
