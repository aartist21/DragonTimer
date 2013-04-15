using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GuildWars2WorldEventTracker;

namespace GuildWars2WorldEventTracker
{
    /// <summary>
    /// Interaction logic for TimerControl.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        public Timer EventTimer { get; set; }
        public string EventName { get; set; }
        public string EventInfo { get; set; }
        public Brush TimerColor { get; set; }
        private readonly ManualResetEvent Announcing = new ManualResetEvent(false);

        public TimerControl()
        {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            EventNameTextBlock.Text = EventName;
            EventInfoTextBlock.Text = EventInfo;
            EventCountdown.Text = EventTimer.Time.Duration().ToString();
            EventCountdown.Fill = TimerColor;
            CreateBackground();

            EventTimer.PropertyChanged += UpdateTimer;
        }

        private void CreateBackground()
        {
            if (Global.Events.ContainsKey(EventName))
            {
                TimerBackground.ImageSource =
                    new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), Global.Events[EventName]));
            }
            else
            {
                EventNameTextBlock.Visibility = Visibility.Visible;
                EventInfoTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void UpdateTimer(object sender, PropertyChangedEventArgs e)
        {
            EventCountdown.Text = EventTimer.Time.Duration().ToString();
            if (EventTimer.Time.Duration() == new TimeSpan(0, Properties.Settings.Default.AnnounceTime, 0) 
                && !IsCompleted.IsChecked
                && (EventTimer.Status == EventStatus.Normal 
                    || (EventTimer.Status == EventStatus.Outdated && !Properties.Settings.Default.IgnoreOutdated))
                && !Announcing.WaitOne(50))
            {
                Announcing.Set();
                new Thread(AnnounceEvent).Start();
            }
        }

        private void AnnounceEvent()
        {
            string announce;

            if (EventTimer.Status == EventStatus.Outdated)
            {
                announce = string.Format("Outdated Timer: {0} ", EventName);
            }
            else
            {
                announce = EventName + " ";

                if (EventInfo.Contains("Time Before"))
                    announce += EventInfo.Replace("Time Before", "") + " will start ";
            }

            switch (Properties.Settings.Default.AnnounceTime)
            {
                case 0:
                    announce = announce.Replace("will start", "");
                    announce += "started";
                    break;
                case 1:
                    announce += "in 1 minute";
                    break;
                default:
                    announce += string.Format("in {0} minutes", Properties.Settings.Default.AnnounceTime);
                    break;
            }

            Speaker.Speak(announce);
            Announcing.Reset();
        }

        private void GridMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsCompleted.IsChecked = !IsCompleted.IsChecked;
            HoverGlow.Opacity = 0;

            var fadeIn = new DoubleAnimation(0.0, 0.5, TimeSpan.FromSeconds(0.5));

            var sb = new Storyboard();
            Storyboard.SetTarget(fadeIn, HoverGlow);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("(Opacity)"));
            sb.Children.Add(fadeIn);

            sb.Begin(); 
        }
    }
}
