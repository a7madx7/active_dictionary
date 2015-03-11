using System;
using System.IO;
using Active_Dictionary.Annotations;
using FirstFloor.ModernUI.Presentation;

namespace Active_Dictionary.Classes
{
    [UsedImplicitly]
    internal class Configuration
    {
        private static readonly string SettingsFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\active-settings.ah";

        public Configuration()
        {
            if (!SettingsFileExist())
            {
                File.Create(SettingsFileLocation);
            }
        }

        /// <summary>
        ///     checks to see if settings fiel exists or no.
        /// </summary>
        /// <returns></returns>
        internal static bool SettingsFileExist()
        {
            return File.Exists(SettingsFileLocation);
        }

        /// <summary>
        ///     saves the user settings to a xml configuration file.
        /// </summary>
        /// <param name="themeChosen">the chosen theme by the user</param>
        /// <param name="colorChosen">the app unified color chosen by the user</param>
        /// <param name="fontSize">the font size to be stores in the persona </param>
        /// <param name="width">the window width modified by the user </param>
        /// <param name="height">the window height modified by the user</param>
        /// <param name="mechanism">the mechanism used to search with</param>
        internal static void SaveUserSettings(Link themeChosen, string colorChosen, string fontSize, string width,
            string height, string mechanism)
        {
            var config = new Configurator();
            try
            {
                if (themeChosen == null)
                {
                    themeChosen = new Link
                    {
                        DisplayName = "Osama El Sayed",
                        Source = new Uri("/Active Dictionary;component/Assets/Osama El Sayed.xaml", UriKind.Relative)
                    };
                }
                var theme = new ConfigEntry
                {
                    Name = "theme",
                    Value = themeChosen.Source.ToString() ?? "/Active Dictionary;component/Assets/Osama El Sayed.xaml"
                };
                var color = new ConfigEntry {Name = "color", Value = colorChosen ?? "#e51400"};
                var font = new ConfigEntry {Name = "fontSize", Value = fontSize ?? "Large"};
                var _width = new ConfigEntry {Name = "width", Value = width ?? "600"};
                var _height = new ConfigEntry {Name = "height", Value = height ?? "800"};
                var mech = new ConfigEntry {Name = "mech", Value = mechanism ?? "after"};

                config.AddEntry(theme);
                config.AddEntry(color);
                config.AddEntry(font);
                config.AddEntry(_width);
                config.AddEntry(_height);
                config.AddEntry(mech);

                DBDataGetter.Mechanism = mechanism;

                config.Save(SettingsFileLocation);
            }
            catch
            {
            }
        }

        internal static string[] LoadUserSettings()
        {
            var config = new Configurator(SettingsFileLocation);
            var theme = "";
            var fontSize = "";
            var width = "";
            var height = "";
            var mechanism = "";
            var color = "";

            try
            {
                var key = config.GetEntriesByName("theme");
                theme = key == null ? "/Active Dictionary;component/Assets/Osama El Sayed.xaml" : key[0].Value;
            }
            catch
            {
            }
            try
            {
                var key = config.GetEntriesByName("color");
                color = key == null ? "#e51400" : key[0].Value;
            }
            catch
            {
            }

            try
            {
                var key = config.GetEntriesByName("fontSize");
                fontSize = key == null ? "large" : key[0].Value;
            }
            catch
            {
            }

            try
            {
                var key = config.GetEntriesByName("width");
                width = key == null ? "600" : key[0].Value;
            }
            catch
            {
            }

            try
            {
                var key = config.GetEntriesByName("height");
                height = key == null ? "700" : key[0].Value;
            }
            catch
            {
            }

            try
            {
                var key = config.GetEntriesByName("mech");
                mechanism = key == null ? "after" : key[0].Value;
            }
            catch
            {
            }

            return new[] {theme, color, fontSize, width, height, mechanism};
        }
    }
}