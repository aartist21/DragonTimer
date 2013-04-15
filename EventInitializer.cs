using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using HtmlAgilityPack;

namespace GuildWars2WorldEventTracker
{
    public static class EventInitializer
    {
        private static readonly DispatcherTimer RefreshTimer = new DispatcherTimer { Interval = new TimeSpan(0,5,0) };

        public delegate void RefreshTimersHandler(object sender, EventArgs e);
        public static event RefreshTimersHandler RefreshTimers;
        public delegate void RefreshedHandler(object sender, EventArgs e);
        public static event RefreshedHandler Refreshed;

        public static void OnRefreshed(EventArgs e)
        {
            RefreshedHandler handler = Refreshed;
            if (handler != null) handler(null, e);
        }

        private static List<TimerControl> Events = new List<TimerControl>();


        public static List<TimerControl> Initialize(bool resetEvents)
        {
            if (resetEvents)
                Events = new List<TimerControl>();

            RefreshTimer.Tick += RefreshTick;

            DemandRefresh(null,new EventArgs());

            return Events;
        }

        public static void DemandRefresh(object sender, EventArgs e)
        {            
            RefreshTick(sender,e);
        }
        
        private static void RefreshTick(object sender, EventArgs e)
        {
            if (RefreshTimers == null) return;

            RefreshTimer.Stop();
            RefreshTimers(null, new EventArgs());
        }

        public static void RefreshSite(dynamic doc)
        {
            if (RefreshTimer.IsEnabled)
                RefreshTimer.Stop();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(doc.documentElement.InnerHtml);
            var htmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='eventTimeBox']");
            foreach (var htmlNode in htmlNodeCollection)
            {
                // Event name
                var name = htmlNode.SelectNodes("p[@class='event-info dragon-name']/text()").First().InnerText;

                // Current timer
                var siteTime = htmlNode.SelectNodes("span[last()-1]").First().InnerHtml;
                // Fix lazy site so TimeSpan won't freak out
                while (siteTime.Length < 9)
                {
                    siteTime = "00:" + siteTime;
                }
                var time = TimeSpan.Parse(siteTime);

                // Addtional info
                var info = htmlNode.SelectNodes("span[last()]").First().InnerHtml;

                // Timer color (#ffffff - normal , #ffff00 - pre-events / window , #ab9575 - outdated)
                var color = htmlNode.SelectNodes("span[last()-1]").First().Attributes.First(a => a.Name == "style").Value;
                // Get the RGB color from the damn style
                color = color.Substring(color.IndexOf('#'));


                
                UpdateTimer(name, time, info, color);
            }

            RefreshTimer.Start();
            OnRefreshed(new EventArgs());
        }

        private static void UpdateTimer(string eventName, TimeSpan time, string info, string color)
        {
            var timerControl = Events.FirstOrDefault(e => e.EventName.Equals(eventName));
            var brush = (Brush)new BrushConverter().ConvertFrom(color);
            EventStatus status;
            switch (color)
            {
                case "#ffff00":
                    status = EventStatus.Window;
                    break;
                case "#ffffff":
                    status = EventStatus.Normal;
                    break;
                default: 
                    status = EventStatus.Outdated;
                    break;
            }

            if (timerControl == null)
            {
                var newEvent = new TimerControl
                                   {
                                       EventName = eventName,
                                       EventTimer = new Timer(time, status),
                                       EventInfo = info,
                                       TimerColor = brush
                                   };
                Events.Add(newEvent);
            }
            else
            {
                timerControl.EventTimer.UpdateTimer(time,status);
                timerControl.TimerColor = brush;
                timerControl.EventInfo = info;
            }
        }
    }
}
