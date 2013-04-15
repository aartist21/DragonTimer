using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildWars2WorldEventTracker
{
    public class Server
    {
        public string ServerName { get; set; }
        public string Link { get; set; }
        public Continent Continent { get; set; }

        public override string ToString()
        {
            return ServerName;
        }
    }

    public enum Continent
    {
        EU,
        US
    }
}
