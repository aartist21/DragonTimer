using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildWars2WorldEventTracker
{
    public static class Global
    {
        public static Dictionary<string,string> Events = new Dictionary<string, string>
                                                             {
                                                                 // English
                                                                 {"The Shatterer" , @".\Images\shatterer.png"},
                                                                 {"Tequatl the Sunless",@".\Images\sunless.png"},
                                                                 {"Claw of Jormag",@".\Images\jormag.png"},
                                                                 {"Shadow Behemoth",@".\Images\behemoth.png"},
                                                                 {"Fire Elemental",@".\Images\elemental.png"},
                                                                 {"Great Jungle Wurm",@".\Images\wurm.png"},
                                                                 {"Megadestroyer",@".\Images\megadestroyer.png"},
                                                                 {"Temple of Grenth",@".\Images\grenth.png"},
                                                                 {"Temple of Balthazar",@".\Images\balthazar.png"},
                                                                 {"Temple of Lyssa",@".\Images\lyssa.png"},
                                                                 {"The Frozen Maw",@".\Images\maw.png"},
                                                                 {"Golem Mark II",@".\Images\golem.png"},

                                                                 // French
                                                                 {"Le Destructeur" , @".\Images\shatterer.png"},
                                                                 {"Tequatl le Sans-soleil",@".\Images\sunless.png"},
                                                                 {"La Griffe de Jormag",@".\Images\jormag.png"},
                                                                 {"Béhémoth des marais",@".\Images\behemoth.png"},
                                                                 {"Élémentaire de feu",@".\Images\elemental.png"},
                                                                 {"Grande guivre de jungle",@".\Images\wurm.png"},
                                                                 {"Mégadestructeur",@".\Images\megadestroyer.png"},
                                                                 {"Temple de Grenth",@".\Images\grenth.png"},
                                                                 {"Temple de Balthazar",@".\Images\balthazar.png"},
                                                                 {"Temple de Lyssa",@".\Images\lyssa.png"},
                                                                 {"Le Gouffre gelé",@".\Images\maw.png"},

                                                                 // German
                                                                 {"Der Zerschmetterer" , @".\Images\shatterer.png"},
                                                                 {"Tequatl der Sonnenlose",@".\Images\sunless.png"},
                                                                 {"Klaue Jormags",@".\Images\jormag.png"},
                                                                 {"Schatten-Behemoth",@".\Images\behemoth.png"},
                                                                 {"Feuerelementar",@".\Images\elemental.png"},
                                                                 {"Riesiger Dschungelwurm",@".\Images\wurm.png"},
                                                                 {"Megazerstörer",@".\Images\megadestroyer.png"},
                                                                 {"Tempel des Grenth",@".\Images\grenth.png"},
                                                                 {"Tempel des Balthasar",@".\Images\balthazar.png"},
                                                                 {"Tempel der Lyssa",@".\Images\lyssa.png"},
                                                                 {"Der gefrorene Schlund",@".\Images\maw.png"},

                                                                 // Spanish
                                                                 {"El Demoledor" , @".\Images\shatterer.png"},
                                                                 {"Tequatl el Sombrío",@".\Images\sunless.png"},
                                                                 {"La Garra de Jormag",@".\Images\jormag.png"},
                                                                 {"Behemoth Sombrío",@".\Images\behemoth.png"},
                                                                 {"Elemental de Fuego",@".\Images\elemental.png"},
                                                                 {"Gran Gusano de la Jungla",@".\Images\wurm.png"},
                                                                 {"Megadestructor",@".\Images\megadestroyer.png"},
                                                                 {"Templo de Grenth",@".\Images\grenth.png"},
                                                                 {"Templo de Balthazar",@".\Images\balthazar.png"},
                                                                 {"Templo de Lyssa",@".\Images\lyssa.png"},
                                                                 {"Las Fauces Heladas",@".\Images\maw.png"}

                                                             };
        public static List<Server> Servers = new List<Server>();

    }
}
