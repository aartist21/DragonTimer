using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using HtmlAgilityPack;

namespace GuildWars2WorldEventTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TimerControl> Timers;
        private string ServerPopulated = Properties.Settings.Default.CurrentServer;

        public MainWindow()
        {
            InitializeComponent();

            var Web = new HtmlWeb();
            var htmlDoc = Web.Load("http://guildwarstemple.com/dragontimer/");
            var htmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='server-list']");
            foreach (var htmlNode in htmlNodeCollection)
            {
                var continent = htmlNode.SelectNodes("h2").First().InnerText.Contains("US") ? Continent.US : Continent.EU;
                var servers = htmlNode.SelectNodes("ul/li/a");
                foreach (var server in servers)
                {
                    var serverName = server.InnerHtml.Replace("&acute;", "'").Replace("&iacute;", "i");
                    var link = server.Attributes.First(a => a.Name == "href").Value;
                    Global.Servers.Add(new Server { ServerName = serverName, Continent = continent, Link = link });
                }
            }

            EventInitializer.RefreshTimers += RefreshClick;
            EventInitializer.Refreshed += UpdateTimers;
            Timers = EventInitializer.Initialize(Properties.Settings.Default.CurrentServer != ServerPopulated);
        }

        private void UpdateTimers(object sender, EventArgs e)
        {
            var collection = new ObservableCollection<TimerControl>();
            Timers = Timers.OrderBy(ev => ev.EventTimer.Time.Duration()).OrderBy(ev => ev.EventTimer.Status).ToList();
            foreach (var timer in Timers)
            {
                collection.Add(timer);
            }

            EventList.ItemsSource = collection;
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            SiteBrowser.Navigate(Global.Servers.First(s => s.ServerName == Properties.Settings.Default.CurrentServer).Link);
        }

        private void WebOnLoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
        {
            EventInitializer.RefreshSite(SiteBrowser.Document);
            ServerPopulated = Properties.Settings.Default.CurrentServer;
        }

        private void EventListOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventList.SelectedIndex = -1;
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            foreach (var timer in Timers)
            {
                timer.IsCompleted.IsChecked = false;
            }
        }

        private void OptionsClick(object sender, RoutedEventArgs e)
        {
            if ((bool)new Options().ShowDialog() && Properties.Settings.Default.CurrentServer != ServerPopulated)
            {
                EventList.ItemsSource = null;
                Timers.ForEach(t => t.EventTimer.Dispose());
                Timers = null;
                Timers = EventInitializer.Initialize(Properties.Settings.Default.CurrentServer != ServerPopulated);
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnQuery(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
                OptionsClick(sender,e);
            else
                RefreshClick(sender,e);
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            new About().ShowDialog();
        }
    }

}
