using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GuildWars2WorldEventTracker
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();

            var server = Global.Servers.First(s => s.ServerName == Properties.Settings.Default.CurrentServer);
            ServerNameComboBox.ItemsSource = Global.Servers.Where(s => s.Continent == server.Continent).OrderBy(s => s.ServerName);
            ServerNameComboBox.SelectedItem = server;
            AnnounceTimeSlider.Value = Properties.Settings.Default.AnnounceTime;
            IgnoreOutdatedCheckBox.IsChecked = Properties.Settings.Default.IgnoreOutdated;

            if (server.Continent == Continent.US)
                USButton.IsChecked = true;
            else
                EUButton.IsChecked = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurrentServer = ((Server) ServerNameComboBox.SelectedItem).ServerName;
            Properties.Settings.Default.AnnounceTime = (int) AnnounceTimeSlider.Value;
            Properties.Settings.Default.IgnoreOutdated = (bool) IgnoreOutdatedCheckBox.IsChecked;
            Properties.Settings.Default.Save();
            DialogResult = true;
        }

        private void RadioButtonClick(object sender, RoutedEventArgs e)
        {
            if ((bool) USButton.IsChecked)
            {
                ServerNameComboBox.ItemsSource = Global.Servers.Where(s => s.Continent == Continent.US).OrderBy(s => s.ServerName);
                ServerNameComboBox.SelectedItem = ServerNameComboBox.Items[0];
            }
            else
            {
                ServerNameComboBox.ItemsSource = Global.Servers.Where(s => s.Continent == Continent.EU).OrderBy(s => s.ServerName);
                ServerNameComboBox.SelectedItem = ServerNameComboBox.Items[0];
            }
        }
    }

    public class IntToMinutesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "0":
                    return "Started";
                case "1":
                    return "1 Minute";
                default:
                    return value.ToString() + " Minutes";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
