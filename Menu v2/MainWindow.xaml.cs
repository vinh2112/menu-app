using Ookii.Dialogs.Wpf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow _obj;

        private int indexOfCurrentEditApp;
        public static MainWindow Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new MainWindow();
                }
                return _obj;
            }
        }

        public int IndexOfCurrentEditApp { get => indexOfCurrentEditApp; set => indexOfCurrentEditApp = value; }


        public string pathXML = System.Windows.Forms.Application.StartupPath + @"\Data.xml";

        DispatcherTimer timer = new DispatcherTimer();
        int countTime = 0;
        public MainWindow()
        {
            InitializeComponent();

            _obj = this;
            InitializeFileXML();
            ReadFileXML();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            isInitialize = false;

            Width = SystemParameters.WorkArea.Width * 0.8;
            Height = SystemParameters.WorkArea.Height * 0.8;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countTime++;
            if (countTime == 2)
            {
                hideNotification();
            }
        }

        bool isInitialize = true;
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewMenu.SelectedIndex == 0)
            {
                if (!isInitialize)
                {
                    if (ScrollSearch.Visibility == Visibility.Hidden)
                    {
                        ScrollSearch.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ScrollViewApp.Visibility = Visibility.Visible;
                        btnAddApp.Visibility = Visibility.Visible;
                    }

                    SearchView.Visibility = Visibility.Visible;
                    PnlSelectedApp.Visibility = Visibility.Hidden;
                    PnlSelectedApp.Children.Clear();

                    lbMenuName.Text = "Your App";
                }
            }
            else
            {
                if (ScrollSearch.Visibility == Visibility.Visible)
                {
                    ScrollSearch.Visibility = Visibility.Hidden;
                }
                SearchView.Visibility = Visibility.Collapsed;
                ScrollViewApp.Visibility = Visibility.Hidden;
                if (ListViewMenu.SelectedIndex == 1)
                {
                    btnAddApp.Visibility = Visibility.Hidden;
                    PnlSelectedApp.Visibility = Visibility.Visible;
                    PnlSelectedApp.Children.Clear();

                    lbMenuName.Text = "Microsoft Office";

                    PnlSelectedApp.Children.Add(new UCOffice());
                }
                if (ListViewMenu.SelectedIndex == 2)
                {
                    btnAddApp.Visibility = Visibility.Hidden;
                    PnlSelectedApp.Visibility = Visibility.Visible;
                    PnlSelectedApp.Children.Clear();

                    lbMenuName.Text = "Window App";

                    PnlSelectedApp.Children.Add(new UCWindowApp());
                }
                if (ListViewMenu.SelectedIndex == 3)
                {
                    btnAddApp.Visibility = Visibility.Hidden;
                    PnlSelectedApp.Visibility = Visibility.Visible;
                    PnlSelectedApp.Children.Clear();

                    lbMenuName.Text = "Tool";

                    PnlSelectedApp.Children.Add(new UCTool());
                }
                if (ListViewMenu.SelectedIndex == 4)
                {
                    btnAddApp.Visibility = Visibility.Hidden;
                    PnlSelectedApp.Visibility = Visibility.Visible;
                    PnlSelectedApp.Children.Clear();

                    lbMenuName.Text = "Web";

                    PnlSelectedApp.Children.Add(new UCWeb());
                }
            }

        }

        #region Events phụ
        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 230;
            animation.To = 60;
            Storyboard.SetTarget(animation, ColumnMenuWidth);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 60;
            animation.To = 230;
            Storyboard.SetTarget(animation, ColumnMenuWidth);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        bool isMaximum = false;
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (!isMaximum)
            {
                this.WindowState = WindowState.Maximized;
                isMaximum = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                isMaximum = false;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Create App
        private void FormAddApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FormAddApp.Visibility = Visibility.Collapsed;
        }

        private void TabAddApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btnAddApp_Click(object sender, RoutedEventArgs e)
        {
            tbAppPath.Text = null;
            tbIconPath.Text = null;
            NameApp.Text = null;
            AppIcon.Source = null;

            CardCreateApp.Visibility = Visibility.Visible;
            btnCreateApp.Visibility = Visibility.Visible;
            CardSaveApp.Visibility = Visibility.Collapsed;
            btnSaveApp.Visibility = Visibility.Collapsed;
            FormAddApp.Visibility = Visibility.Visible;
        }

        private void tbAppPath_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tbAppPath.Text != "" && File.Exists(tbAppPath.Text))
            {
                if (tbIconPath.Text == "")
                {
                    Icon ico = IconExtractor2.GetIcon(tbAppPath.Text, false, false);
                    AppIcon.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                else
                {
                    if (File.Exists(tbIconPath.Text))
                    {
                        var bin = File.ReadAllBytes(tbIconPath.Text);
                        ImageSourceConverter con = new ImageSourceConverter();
                        AppIcon.Source = (ImageSource)con.ConvertFrom(bin);
                    }
                }
                FileVersionInfo myFile = FileVersionInfo.GetVersionInfo(tbAppPath.Text);
                NameApp.Text = myFile.ProductName;
            }
            else
            {
                if (!File.Exists(tbIconPath.Text))
                {
                    AppIcon.Source = null;
                }
            }

        }
        private void tbIconPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbIconPath.Text != "")
            {
                if (File.Exists(tbIconPath.Text))
                {
                    var bin = File.ReadAllBytes(tbIconPath.Text);
                    ImageSourceConverter con = new ImageSourceConverter();
                    AppIcon.Source = (ImageSource)con.ConvertFrom(bin);
                }
            }
            else
            {
                Icon ico = IconExtractor2.GetIcon(tbAppPath.Text, false, false);
                AppIcon.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void btnCreateApp_Click(object sender, RoutedEventArgs e)
        {
            if (tbAppPath.Text != null && File.Exists(tbAppPath.Text))
            {
                CreateAppXML();

                txtNotification.Text = "Created";
                showNotification();
            }
        }
        private void btnSaveApp_Click(object sender, RoutedEventArgs e)
        {
            if (tbAppPath.Text != null && File.Exists(tbAppPath.Text))
            {
                SaveFileXML();

                txtNotification.Text = "Edited";
                showNotification();
            }
        }

        private void btnChooseApp_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog filePath = new VistaOpenFileDialog();
            filePath.Title = "Application";
            filePath.Filter = "All Files(*.*)|*.*";
            if (filePath.ShowDialog().GetValueOrDefault())
            {
                tbAppPath.Text = filePath.FileName;
            }
        }

        private void btnChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog filePath = new VistaOpenFileDialog();
            filePath.Filter = "Image Files(*.jpg;*.jpeg;*.jpe;*.jfif;*.png;*.ico)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png;*.ico|All Files (*.*)|*.*";
            filePath.Title = "Picture";
            if (filePath.ShowDialog().GetValueOrDefault())
            {
                tbIconPath.Text = filePath.FileName;
                var bin = File.ReadAllBytes(filePath.FileName);
                ImageSourceConverter con = new ImageSourceConverter();
                AppIcon.Source = (ImageSource)con.ConvertFrom(bin);
            }
        }
        #endregion

        #region XMLFile
        public void InitializeFileXML()
        {
            bool isExisted = File.Exists(pathXML);
            if (!isExisted)
            {
                XmlDocument doc = new XmlDocument();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement mainElement = doc.CreateElement(string.Empty, "Menu", string.Empty);
                doc.AppendChild(mainElement);

                XmlElement Data = doc.CreateElement(string.Empty, "Data", string.Empty);
                mainElement.AppendChild(Data);

                doc.Save(pathXML);
            }
        }
        public void ReadFileXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXML);
            XmlElement root = doc.DocumentElement;

            XmlNode data = root.SelectSingleNode("Data");
            XmlNodeList App = data.SelectNodes("App");

            foreach (XmlNode item in App)
            {
                string nameApp = item.SelectSingleNode("Name").InnerText;
                string pathApp = item.SelectSingleNode("PathApp").InnerText;
                string iconApp = item.SelectSingleNode("PathImage").InnerText;

                UCApplication uc = new UCApplication(nameApp, pathApp, iconApp);
                PnlApplications.Children.Add(uc);
            }
        }
        void CreateAppXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXML);
            XmlElement root = doc.DocumentElement;
            XmlNode data = root.SelectSingleNode("Data");

            XmlElement App = doc.CreateElement(string.Empty, "App", string.Empty);

            XmlElement nameApp = doc.CreateElement(string.Empty, "Name", string.Empty);
            nameApp.InnerText = NameApp.Text;
            XmlElement pathApp = doc.CreateElement(string.Empty, "PathApp", string.Empty);
            pathApp.InnerText = tbAppPath.Text;
            XmlElement pathImage = doc.CreateElement(string.Empty, "PathImage", string.Empty);
            pathImage.InnerText = tbIconPath.Text;

            App.AppendChild(nameApp);
            App.AppendChild(pathApp);
            App.AppendChild(pathImage);

            data.AppendChild(App);

            doc.Save(pathXML);

            PnlApplications.Children.Add(new UCApplication(NameApp.Text, tbAppPath.Text, tbIconPath.Text));
            NameApp.Clear();
            tbAppPath.Clear();
            tbIconPath.Clear();
            AppIcon.Source = null;

            FormAddApp.Visibility = Visibility.Collapsed;
        }

        void SaveFileXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXML);

            XmlElement root = doc.DocumentElement;
            XmlNode data = root.SelectSingleNode("Data");
            XmlNodeList app = data.SelectNodes("App");

            app[IndexOfCurrentEditApp]["Name"].InnerText = NameApp.Text;
            app[IndexOfCurrentEditApp]["PathApp"].InnerText = tbAppPath.Text;
            app[IndexOfCurrentEditApp]["PathImage"].InnerText = tbIconPath.Text;

            doc.Save(pathXML);

            PnlApplications.Children.Clear();
            ReadFileXML();

            FormAddApp.Visibility = Visibility.Collapsed;
        }

        public void RemoveAppXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXML);

            XmlElement root = doc.DocumentElement;
            XmlNode data = root.SelectSingleNode("Data");
            XmlNodeList app = data.SelectNodes("App");

            data.RemoveChild(app[IndexOfCurrentEditApp]);

            doc.Save(pathXML);
        }
        #endregion

        #region Notification
        public void showNotification()
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseInOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 0;
            animation.To = 1;
            Storyboard.SetTarget(animation, Notification);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Opacity)"));

            storyboard.Begin();
            countTime = 0;
            timer.Start();
        }

        public void hideNotification()
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseInOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 1;
            animation.To = 0;
            Storyboard.SetTarget(animation, Notification);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Opacity)"));

            storyboard.Begin();

            timer.Stop();
        }

        #endregion

        #region Search
        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            btnAddApp.Focus();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text.Length != 0)
            {
                PnlSearchApps.Children.Clear();
                btnClearSearch.Visibility = Visibility.Visible;
                ScrollSearch.Visibility = Visibility.Visible;
                ScrollViewApp.Visibility = Visibility.Collapsed;
                btnAddApp.Visibility = Visibility.Collapsed;
                foreach (UCApplication item in PnlApplications.Children)
                {
                    string nameApp = ConvertVietnamese(item.lbNameApp.Text).Trim();
                    string textSearch = ConvertVietnamese(tbSearch.Text).Trim();
                    if (nameApp.Contains(textSearch))
                    {
                        PnlSearchApps.Children.Add(new UCApplication(item.NameApp, item.PathApp, item.PathIcon));
                    }
                }
            }
            else
            {
                btnClearSearch.Visibility = Visibility.Collapsed;
                ScrollSearch.Visibility = Visibility.Collapsed;
                ScrollViewApp.Visibility = Visibility.Visible;
                btnAddApp.Visibility = Visibility.Visible;
            }
        }
        string ConvertVietnamese(string s)
        {
            s = s.ToLower();
            s = Regex.Replace(s, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            s = Regex.Replace(s, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            s = Regex.Replace(s, "ì|í|ị|ỉ|ĩ|/g", "i");
            s = Regex.Replace(s, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            s = Regex.Replace(s, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            s = Regex.Replace(s, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            s = Regex.Replace(s, "đ", "d");
            return s;
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbSearch.Text.Length != 0)
            {
                if (e.Key == Key.Escape)
                {
                    tbSearch.Text = "";
                }
            }
        }

        #endregion
    }
}
