using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace GuildWars2WorldEventTracker
{
    public class Timer : INotifyPropertyChanged, IDisposable
    {
        private DispatcherTimer TimerCountdown;
        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; NotifyPropertyChanged("Time"); }
        }
        public EventStatus Status { get; set; }
        
        public Timer(TimeSpan time, EventStatus status)
        {
            Time = time;
            Status = status;

            TimerCountdown = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            TimerCountdown.Tick += Countdown;
            TimerCountdown.Start();
        }

        private void Countdown(object sender, EventArgs eventArgs)
        {
            if (Time.TotalSeconds > 0)
            {
                Time = Time.Subtract(new TimeSpan(0, 0, 1));
            }
            else
            {
                TimerCountdown.Stop();
                EventInitializer.DemandRefresh(this,new EventArgs());
            }

        }

        public void UpdateTimer(TimeSpan newTime, EventStatus status)
        {
            TimerCountdown.Stop();
            Time = newTime;
            Status = status;
            TimerCountdown.Start();
        }

        public override string ToString()
        {
            return Time.Duration().ToString();
        }

        public void Dispose()
        {
            TimerCountdown.Tick -= Countdown;
            TimerCountdown.Stop();
            TimerCountdown = null;
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
