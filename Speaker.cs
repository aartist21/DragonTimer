using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Speech;

namespace GuildWars2WorldEventTracker
{
    public static class Speaker
    {
        private static SpeechSynthesizer _speaker = new SpeechSynthesizer() {Rate = -1};

        public static void Speak(string text)
        {
            _speaker.Speak(text);
        }
    }
}
