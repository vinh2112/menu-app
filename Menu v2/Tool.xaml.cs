using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Menu_v2
{
    /// <summary>
    /// Interaction logic for Tool.xaml
    /// </summary>
    public partial class Tool : Window
    {
        static Tool _obj;
        DispatcherTimer timer = new DispatcherTimer();
        private int count;
        bool isClose = false;
        public static Tool Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new Tool();
                }
                return _obj;
            }
        }
        public Tool()
        {
            InitializeComponent();
            _obj = this;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count > 1)
            {
                HideCopiedWifi();
            }
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            PnlWifi.Children.Clear();
            string cmdCommand = "netsh wlan show profiles";

            Process cmd = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            cmd.StartInfo = startInfo;
            cmd.Start();

            cmd.StandardInput.WriteLine(cmdCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();


            string result = cmd.StandardOutput.ReadToEnd();
            Regex regex = new Regex(@"All User Profile     : (?<Wifi>.+)");
            Match str;
            foreach (Match item in regex.Matches(result))
            {
                string nameWifi = item.Groups["Wifi"].ToString().Trim();

                //Đưa SSID vào " "
                string addstr = item.Groups["Wifi"].ToString().Trim();
                addstr = addstr.Insert(0, '"'.ToString());
                addstr = addstr.Insert(addstr.Length, '"'.ToString());

                //Thực thi lệnh newcommand
                string newcommand = "netsh wlan show profile  " + addstr + " key=clear | findstr Key";
                cmd.Start();
                cmd.StandardInput.WriteLine(newcommand);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();

                //Lấy Password trong chuỗi
                string password = cmd.StandardOutput.ReadToEnd();
                regex = new Regex(@"Key Content            : (?<Password>.+)");
                str = regex.Match(password);

                string passWifi = str.Groups["Password"].ToString();

                UCWifi uc = new UCWifi(nameWifi, passWifi);
                PnlWifi.Children.Add(uc);
            }

        }

        public void ShowCopiedWifi()
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(100));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseInOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 0;
            animation.To = 40;
            Storyboard.SetTarget(animation, TextCopied);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Height)"));

            storyboard.Begin();
            count = 0;
            timer.Start();
        }

        public void HideCopiedWifi()
        {
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseIn };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 40;
            animation.To = 0;
            Storyboard.SetTarget(animation, TextCopied);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Height)"));

            storyboard.Begin();

            timer.Stop();
        }

        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuList.SelectedIndex == 0 && isClose)
            {
                Storyboard storyboard = new Storyboard();

                Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
                CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseInOut };

                DoubleAnimation animation = new DoubleAnimation();
                animation.EasingFunction = ease;
                animation.Duration = duration;
                storyboard.Children.Add(animation);
                animation.From = 0;
                animation.To = 410;
                Storyboard.SetTarget(animation, WifiOption);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(Height)"));

                storyboard.Begin();
                isClose = false;
                WifiOption.Visibility = Visibility.Visible;
                AlarmOption.Visibility = Visibility.Hidden;
            }
            else
            {
                if (MenuList.SelectedIndex == 1 && !isClose)
                {
                    Storyboard storyboard = new Storyboard();

                    Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
                    CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseInOut };

                    DoubleAnimation animation = new DoubleAnimation();
                    animation.EasingFunction = ease;
                    animation.Duration = duration;
                    storyboard.Children.Add(animation);
                    animation.From = 410;
                    animation.To = 0;
                    Storyboard.SetTarget(animation, WifiOption);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(Height)"));

                    storyboard.Begin();
                    isClose = true;
                    WifiOption.Visibility = Visibility.Hidden;
                    AlarmOption.Visibility = Visibility.Visible;
                }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (int.Parse(e.Text) > 60)
            {
                e.Handled = true;
            }
        }

        void HourPlus()
        {
            if (tbHour.Text == "")
            {
                tbHour.Text = "01";
            }
            else
            {
                int hour = int.Parse(tbHour.Text);
                hour++;
                if (hour < 10)
                {
                    tbHour.Text = "0" + hour.ToString();
                }
                else
                {
                    if (hour < 100)
                    {
                        tbHour.Text = hour.ToString();
                    }
                }
            }
        }
        private void HourPlus_Click(object sender, RoutedEventArgs e)
        {
            HourPlus();
        }

        void MinutePlus()
        {
            if (tbMinute.Text == "")
            {
                tbMinute.Text = "01";
            }
            else
            {
                int minute = int.Parse(tbMinute.Text);
                minute++;
                if (minute < 10)
                {
                    tbMinute.Text = "0" + minute.ToString();
                }
                else
                {
                    if (minute < 60)
                    {
                        tbMinute.Text = minute.ToString();
                    }
                    else
                    {

                        if (tbHour.Text != "")
                        {
                            if (int.Parse(tbHour.Text) < 99)
                            {
                                HourPlus();
                                tbMinute.Text = "00";
                            }
                        }
                        else
                        {
                            tbHour.Text = "01";
                            tbMinute.Text = "00";
                        }
                    }
                }
            }
        }
        private void MinutePlus_Click(object sender, RoutedEventArgs e)
        {
            MinutePlus();
        }

        private void SecondPlus_Click(object sender, RoutedEventArgs e)
        {
            if (tbSecond.Text == "")
            {
                tbSecond.Text = "01";
            }
            else
            {
                int second = int.Parse(tbSecond.Text);
                second++;
                if (second < 10)
                {
                    tbSecond.Text = "0" + second.ToString();
                }
                else
                {
                    if (second < 60)
                    {
                        tbSecond.Text = second.ToString();
                    }
                    else
                    {
                        if (tbMinute.Text == "")
                        {
                            tbMinute.Text = "01";
                            tbSecond.Text = "00";
                        }
                        else
                        {
                            if (tbHour.Text != "")
                            {
                                if (int.Parse(tbHour.Text) == 99 && int.Parse(tbMinute.Text) == 59)
                                {

                                }
                                else
                                {
                                    MinutePlus();
                                    tbSecond.Text = "00";
                                }
                            }
                            else
                            {
                                MinutePlus();
                                tbSecond.Text = "00";
                            }
                        }

                    }
                }
            }
        }

        private void btnHourMinus_Click(object sender, RoutedEventArgs e)
        {
            if (tbHour.Text == "")
            {
                tbHour.Text = "00";
            }
            else
            {
                int hour = int.Parse(tbHour.Text);
                if (hour > 0)
                {
                    hour--;
                    if (hour < 10)
                    {
                        tbHour.Text = "0" + hour.ToString();
                    }
                    else
                    {
                        tbHour.Text = hour.ToString();
                    }
                }
            }
        }

        private void btnMiniteMinus_Click(object sender, RoutedEventArgs e)
        {
            if (tbMinute.Text == "")
            {
                if (tbHour.Text == "")
                {
                    tbMinute.Text = "00";
                }
                else
                {
                    //int minute = int.Parse(tbMinute.Text);
                    int hour = int.Parse(tbHour.Text);
                    if (hour > 0)
                    {
                        if (hour < 10)
                        {
                            tbHour.Text = "0" + (--hour).ToString();
                            tbMinute.Text = "59";
                        }
                        else
                        {
                            tbHour.Text = (--hour).ToString();
                            tbMinute.Text = "59";
                        }
                    }
                }
            }
            else
            {
                int minute = int.Parse(tbMinute.Text);
                minute--;
                if (minute < 0)
                {
                    if (tbHour.Text != "")
                    {
                        int hour = int.Parse(tbHour.Text);
                        hour--;
                        if (hour >= 0)
                        {
                            if (hour < 10)
                            {
                                tbHour.Text = "0" + hour.ToString();
                                tbMinute.Text = "59";
                            }
                            else
                            {
                                tbHour.Text = hour.ToString();
                                tbMinute.Text = "59";
                            }
                        }
                    }
                }
                else
                {
                    if (minute < 10)
                    {
                        tbMinute.Text = "0" + minute.ToString();
                    }
                    else
                    {
                        tbMinute.Text = minute.ToString();
                    }
                }

            }
        }

        private void btnSecondMinus_Click(object sender, RoutedEventArgs e)
        {
            int second = 0;
            try
            {
                second = int.Parse(tbSecond.Text);
            }
            catch { second = 60; }
            second--;
            if (second >= 0)
            {
                if (second < 10)
                {
                    tbSecond.Text = "0" + second.ToString();
                }
                else
                {
                    tbSecond.Text = second.ToString();
                }
            }
            else
            {
                if (tbHour.Text == "")
                {
                    if (tbMinute.Text != "" && int.Parse(tbMinute.Text) > 0)
                    {
                        int minute = int.Parse(tbMinute.Text);
                        minute--;
                        if (minute == 0)
                        {
                            tbMinute.Text = "00";
                            tbSecond.Text = "59";
                        }
                        else
                        {
                            tbMinute.Text = minute.ToString();
                            tbSecond.Text = "59";
                        }
                    }
                }
                else
                {
                    if (tbMinute.Text != "")
                    {
                        int minute = int.Parse(tbMinute.Text);
                        int hour = int.Parse(tbHour.Text);
                        minute--;
                        if (minute >= 0)
                        {
                            if (minute < 10)
                            {
                                tbMinute.Text = "0" + minute.ToString();
                                tbSecond.Text = "59";
                            }
                            else
                            {
                                tbMinute.Text = minute.ToString();
                                tbSecond.Text = "59";
                            }
                        }
                        else
                        {
                            if (hour > 0)
                            {
                                hour--;
                                if (hour < 10)
                                {
                                    tbHour.Text = "0" + hour.ToString();
                                    tbMinute.Text = "59";
                                    tbSecond.Text = "59";
                                }
                                else
                                {
                                    tbHour.Text = hour.ToString();
                                    tbMinute.Text = "59";
                                    tbSecond.Text = "59";
                                }
                            }
                        }

                    }
                }
            }

        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            int time = 0;
            if (tbHour.Text != "")
            {
                time += (int.Parse(tbHour.Text) * 60 * 60);
            }
            if (tbMinute.Text != "")
            {
                time += (int.Parse(tbMinute.Text) * 60);
            }
            if (tbSecond.Text != "")
            {
                time += int.Parse(tbSecond.Text);
            }
            Process.Start("shutdown", "/s /t " + time.ToString());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "/a");
        }

    }
}
