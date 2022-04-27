using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for UCApplication.xaml
    /// </summary>
    public partial class UCApplication : UserControl
    {
        private string nameApp;
        private string pathApp;
        private string pathIcon;
        public UCApplication(string name, string path, string pathicon)
        {
            InitializeComponent();
            lbNameApp.Text = name;
            this.NameApp = name;
            this.PathApp = path;
            this.PathIcon = pathicon;

            if(File.Exists(@pathicon))
            {
                var bin = File.ReadAllBytes(@pathicon);
                ImageSourceConverter con = new ImageSourceConverter();
                ImageApp.Source = (ImageSource)con.ConvertFrom(bin);
            }
            else
            {
                try
                {
                    Icon ico = IconExtractor2.GetIcon(path, false, false);
                    ImageApp.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch { }
            }
        }

        public string NameApp { get => nameApp; set => nameApp = value; }
        public string PathApp { get => pathApp; set => pathApp = value; }
        public string PathIcon { get => pathIcon; set => pathIcon = value; }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.IndexOfCurrentEditApp = MainWindow.Instance.PnlApplications.Children.IndexOf(this);

            MainWindow.Instance.FormAddApp.Visibility = Visibility.Visible;
            MainWindow.Instance.CardCreateApp.Visibility = Visibility.Collapsed;
            MainWindow.Instance.btnCreateApp.Visibility = Visibility.Collapsed;
            MainWindow.Instance.CardSaveApp.Visibility = Visibility.Visible;
            MainWindow.Instance.btnSaveApp.Visibility = Visibility.Visible;

            MainWindow.Instance.tbAppPath.Text = PathApp;
            MainWindow.Instance.NameApp.Text = NameApp;
            MainWindow.Instance.tbIconPath.Text = PathIcon;

            MainWindow.Instance.AppIcon.Source = ImageApp.Source;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(PathApp);
            MainWindow.Instance.WindowState = WindowState.Minimized;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.IndexOfCurrentEditApp = MainWindow.Instance.PnlApplications.Children.IndexOf(this);

            MainWindow.Instance.RemoveAppXML();

            MainWindow.Instance.PnlApplications.Children.Remove(this);

            MainWindow.Instance.txtNotification.Text = "Removed App";
            MainWindow.Instance.showNotification();
        }

        private void MovetoTop_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(MainWindow.Instance.pathXML);

            XmlElement root = doc.DocumentElement;
            XmlNode data = root.SelectSingleNode("Data");
            XmlNodeList app = data.SelectNodes("App");

            XmlNode tempNode = app[MainWindow.Instance.PnlApplications.Children.IndexOf(this)];
            XmlElement App = doc.CreateElement(string.Empty, "App", string.Empty);

            XmlElement nameApp = doc.CreateElement(string.Empty, "Name", string.Empty);
            nameApp.InnerText = tempNode["Name"].InnerText;
            XmlElement pathApp = doc.CreateElement(string.Empty, "PathApp", string.Empty);
            pathApp.InnerText = tempNode["PathApp"].InnerText;
            XmlElement pathImage = doc.CreateElement(string.Empty, "PathImage", string.Empty);
            pathImage.InnerText = tempNode["PathImage"].InnerText;

            App.AppendChild(nameApp);
            App.AppendChild(pathApp);
            App.AppendChild(pathImage);

            data.PrependChild(App);
            data.RemoveChild(app[MainWindow.Instance.PnlApplications.Children.IndexOf(this)]);

            doc.Save(MainWindow.Instance.pathXML);

            MainWindow.Instance.PnlApplications.Children.Clear();
            MainWindow.Instance.ReadFileXML();

            MainWindow.Instance.txtNotification.Text = "Moved to Top";
            MainWindow.Instance.showNotification();
        }
    }
}
