// Copyright (c) 2014 Ravi Bhavnani
// License: Code Project Open License
// http://www.codeproject.com/info/cpol10.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RavSoft;

namespace Active_Dictionary.Models
{
    /// <summary>
    ///     Translates text using Google's online language tools.
    /// </summary>
    public class Translator : WebResourceProvider
    {
        #region Fields

        /// <summary>
        ///     The language to translation mode map.
        /// </summary>
        private static Dictionary<string, string> _languageModeMap;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Translator" /> class.
        /// </summary>
        public Translator()
        {
            SourceLanguage = "English";
            TargetLanguage = "French";
            Referer = "http://translate.google.com/";
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Attempts to translate the text.
        /// </summary>
        public void Translate()
        {
            // Validate source and target languages
            if (string.IsNullOrEmpty(SourceLanguage) ||
                string.IsNullOrEmpty(TargetLanguage) ||
                SourceLanguage.Trim().Equals(TargetLanguage.Trim()))
            {
                throw new Exception("An invalid source or target language was specified.");
            }

            // Delegate to base class
            var start = DateTime.Now;
            this.FetchResource();
            TranslationTime = DateTime.Now - start;
        }

        public Task TranslateAsync()
        {
            return Task.Run(() => Translate());
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the source language.
        /// </summary>
        /// <value>The source language.</value>
        public string SourceLanguage { get; set; }

        /// <summary>
        ///     Gets or sets the target language.
        /// </summary>
        /// <value>The target language.</value>
        public string TargetLanguage { get; set; }

        /// <summary>
        ///     Gets or sets the source text.
        /// </summary>
        /// <value>The source text.</value>
        public string SourceText { get; set; }

        /// <summary>
        ///     Gets the translation.
        /// </summary>
        /// <value>The translated text.</value>
        public string Translation { get; private set; }

        /// <summary>
        ///     Gets the url used to speak the translation.
        /// </summary>
        /// <value>The url used to speak the translation.</value>
        public string TranslationSpeakUrl { get; private set; }

        /// <summary>
        ///     Gets the time taken to perform the translation.
        /// </summary>
        /// <value>The time taken to perform the translation.</value>
        public TimeSpan TranslationTime { get; private set; }

        /// <summary>
        ///     Gets the supported languages.
        /// </summary>
        public static IEnumerable<string> Languages
        {
            get
            {
                EnsureInitialized();
                return _languageModeMap.Keys.OrderBy(p => p);
            }
        }

        #endregion

        #region WebResourceProvider implementation

        /// <summary>
        ///     Returns the url to be fetched.
        /// </summary>
        /// <returns>The url to be fetched.</returns>
        protected override string GetFetchUrl()
        {
            var url =
                string.Format(
                    "http://translate.google.com/translate_a/t?client=t&sl={0}&tl={1}&ie=UTF-8&oe=UTF-8&q={2}",
                    LanguageEnumToIdentifier(SourceLanguage),
                    LanguageEnumToIdentifier(TargetLanguage),
                    HttpUtility.UrlEncode(SourceText));
            return url;
        }

        /// <summary>
        ///     Parses the fetched content.
        /// </summary>
        protected override void ParseContent()
        {
            // Initialize the parser
            Translation = string.Empty;
            var strContent = Content;
            RavSoft.StringParser parser = new RavSoft.StringParser(strContent);

            // Extract the translation
            var strTranslation = string.Empty;
            if (parser.skipToEndOf("[[[\""))
            {
                var morePhrasesRemaining = true;
                do
                {
                    string translatedPhrase = null;
                    if (parser.extractTo("\",\"", ref translatedPhrase))
                    {
                        strTranslation += translatedPhrase;
                    }
                    morePhrasesRemaining = parser.skipToEndOf(",\"\",\"\"],[\"");
                } while (morePhrasesRemaining);
            }
            Translation =
                strTranslation.Replace(" .", ".")
                    .Replace(" ?", "?")
                    .Replace(" ,", ",")
                    .Replace(" ;", ";")
                    .Replace(" !", "!");

            // Set the translation speak url
            TranslationSpeakUrl = string.Format("http://translate.google.com/translate_tts?ie=UTF-8&tl={0}&q={1}",
                LanguageEnumToIdentifier(TargetLanguage),
                HttpUtility.UrlEncode(Translation));
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Converts a language to its identifier.
        /// </summary>
        /// <param name="language">The language."</param>
        /// <returns>The identifier or <see cref="string.Empty" /> if none.</returns>
        private static string LanguageEnumToIdentifier
            (string language)
        {
            var mode = string.Empty;
            EnsureInitialized();
            _languageModeMap.TryGetValue(language, out mode);
            return mode;
        }

        /// <summary>
        ///     Ensures the translator has been initialized.
        /// </summary>
        private static void EnsureInitialized()
        {
            if (_languageModeMap == null)
            {
                _languageModeMap = new Dictionary<string, string>
                {
                    {"Afrikaans", "af"},
                    {"Albanian", "sq"},
                    {"Arabic", "ar"},
                    {"Armenian", "hy"},
                    {"Azerbaijani", "az"},
                    {"Basque", "eu"},
                    {"Belarusian", "be"},
                    {"Bengali", "bn"},
                    {"Bulgarian", "bg"},
                    {"Catalan", "ca"},
                    {"Chinese", "zh-CN"},
                    {"Croatian", "hr"},
                    {"Czech", "cs"},
                    {"Danish", "da"},
                    {"Dutch", "nl"},
                    {"English", "en"},
                    {"Esperanto", "eo"},
                    {"Estonian", "et"},
                    {"Filipino", "tl"},
                    {"Finnish", "fi"},
                    {"French", "fr"},
                    {"Galician", "gl"},
                    {"German", "de"},
                    {"Georgian", "ka"},
                    {"Greek", "el"},
                    {"Haitian Creole", "ht"},
                    {"Hebrew", "iw"},
                    {"Hindi", "hi"},
                    {"Hungarian", "hu"},
                    {"Icelandic", "is"},
                    {"Indonesian", "id"},
                    {"Irish", "ga"},
                    {"Italian", "it"},
                    {"Japanese", "ja"},
                    {"Korean", "ko"},
                    {"Lao", "lo"},
                    {"Latin", "la"},
                    {"Latvian", "lv"},
                    {"Lithuanian", "lt"},
                    {"Macedonian", "mk"},
                    {"Malay", "ms"},
                    {"Maltese", "mt"},
                    {"Norwegian", "no"},
                    {"Persian", "fa"},
                    {"Polish", "pl"},
                    {"Portuguese", "pt"},
                    {"Romanian", "ro"},
                    {"Russian", "ru"},
                    {"Serbian", "sr"},
                    {"Slovak", "sk"},
                    {"Slovenian", "sl"},
                    {"Spanish", "es"},
                    {"Swahili", "sw"},
                    {"Swedish", "sv"},
                    {"Tamil", "ta"},
                    {"Telugu", "te"},
                    {"Thai", "th"},
                    {"Turkish", "tr"},
                    {"Ukrainian", "uk"},
                    {"Urdu", "ur"},
                    {"Vietnamese", "vi"},
                    {"Welsh", "cy"},
                    {"Yiddish", "yi"}
                };
            }
        }

        #endregion
    }
}