using System.Speech.Synthesis;

namespace Active_Dictionary.Classes
{
    internal class Speaker
    {
        private static Speaker _instance;
        private readonly SpeechSynthesizer _speakerSynthesizer = new SpeechSynthesizer();

        public static Speaker Current
        {
            get { return _instance ?? (_instance = new Speaker()); }
        }

        /// <summary>
        ///     Says the word given to it.
        /// </summary>
        /// <param name="textToSay">the text to be said.</param>
        internal void Say(string textToSay)
        {
            // Speak
            _speakerSynthesizer.SpeakAsync(textToSay);
            // 0 to 100
            _speakerSynthesizer.Volume = 100;
            // -10 to 10
            _speakerSynthesizer.Rate = -2;
        }
    }
}