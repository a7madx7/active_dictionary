using System;
using System.ComponentModel;
using System.Windows.Media;
using Active_Dictionary.Classes;
using Active_Dictionary.Content;
using FirstFloor.ModernUI.Presentation;

namespace Active_Dictionary.ViewModels
{
    /// <summary>
    ///     A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SettingsAppearanceViewModel
        : NotifyPropertyChanged
    {
        private const string FontSmall = "small";
        private const string FontLarge = "large";
        private static bool _isInstantiated;
        internal static SettingsAppearanceViewModel GetInstance;
        private static string _mech;
        public static string Width;
        public static string Height;
        public static Link StaticSelectedTheme;
        public static string StaticSelectedFont;
        private Color _selectedAccentColor;
        private string _selectedFontSize;
        private Link _selectedTheme;

        public SettingsAppearanceViewModel(Link theme, string color, string fontSize, string width, string height,
            string mechanism)
        {
            try
            {
                if (_isInstantiated) return;

                SetDefaults(theme, color, fontSize);

                //this.selectedTheme = theme;
                _selectedFontSize = fontSize;
                Width = width;
                Height = height;
                Mechanism = mechanism;
                //if (string.IsNullOrEmpty(color)) color = "#e51400";

                //var convertFromString = ColorConverter.ConvertFromString(color);

                //if (convertFromString != null)
                //    this.SelectedAccentColor = (Color)convertFromString;

                _isInstantiated = true;
                GetInstance = this;
            }
            catch
            {
            }
        }

        public SettingsAppearanceViewModel()
        {
            // nothing to be done here
            // you may want to call SetDefault();
        }

        public static string Mechanism
        {
            get
            {
                if (string.IsNullOrEmpty(_mech))
                {
                    try
                    {
                        return Configuration.LoadUserSettings()[5];
                    }
                    catch
                    {
                    }
                }
                return _mech;
            }
            set { _mech = value; }
        }

        public static Color StaticSelectedColor { get; set; }
        /* ##############################################*/

        public LinkCollection Themes = new LinkCollection();

        public string[] FontSizes
        {
            get { return new[] {FontSmall, FontLarge}; }
        }

        public Color[] AccentColors = {
            Color.FromRgb(0xa4, 0xc4, 0x00), // lime
            Color.FromRgb(0x60, 0xa9, 0x17), // green
            Color.FromRgb(0x00, 0x8a, 0x00), // emerald
            Color.FromRgb(0x00, 0xab, 0xa9), // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2), // cyan
            Color.FromRgb(0x00, 0x50, 0xef), // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff), // indigo
            Color.FromRgb(0xaa, 0x00, 0xff), // violet
            Color.FromRgb(0xf4, 0x72, 0xd0), // pink
            Color.FromRgb(0xd8, 0x00, 0x73), // magenta
            Color.FromRgb(0xa2, 0x00, 0x25), // crimson
            Color.FromRgb(0xe5, 0x14, 0x00), // red
            Color.FromRgb(0xfa, 0x68, 0x00), // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a), // amber
            Color.FromRgb(0xe3, 0xc8, 0x00), // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c), // brown
            Color.FromRgb(0x6d, 0x87, 0x64), // olive
            Color.FromRgb(0x64, 0x76, 0x87), // steel
            Color.FromRgb(0x76, 0x60, 0x8a), // mauve
            Color.FromRgb(0x87, 0x79, 0x4e) // taupe
        };

        public Link SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme == value) return;
                _selectedTheme = value;
                StaticSelectedTheme = value;
                AppearanceManager.Current.ThemeSource = value.Source;

                OnPropertyChanged("SelectedTheme");
                //Configuration.SaveUserSettings(value, SelectedAccentColor.ToString(), SelectedFontSize,
                //                       Width, Height, Mechanism);

                // and update the actual theme
            }
        }

        public string SelectedFontSize
        {
            get { return _selectedFontSize; }
            set
            {
                if (_selectedFontSize == value) return;
                _selectedFontSize = value;

                //Configuration.SaveUserSettings(selectedTheme, SelectedAccentColor.ToString(), SelectedFontSize,
                //                    Width, Height, Mechanism);
                StaticSelectedFont = value;

                AppearanceManager.Current.FontSize = (value == FontLarge) ? FontSize.Large : FontSize.Small;
                OnPropertyChanged("SelectedFontSize");
            }
        }

        public Color SelectedAccentColor
        {
            get { return _selectedAccentColor; }
            set
            {
                if (_selectedAccentColor == value) return;
                _selectedAccentColor = value;

                //Configuration.SaveUserSettings(selectedTheme, SelectedAccentColor.ToString(), SelectedFontSize,
                //                    Width, Height, Mechanism);

                StaticSelectedColor = value;

                AppearanceManager.Current.AccentColor = value;
                OnPropertyChanged("SelectedAccentColor");
            }
        }

        private void SetDefaults(Link theme, string color, string fontSize)
        {
            Themes.Clear();
            // add the default themes
            Themes.Add(new Link {DisplayName = "dark", Source = AppearanceManager.DarkThemeSource});
            Themes.Add(new Link {DisplayName = "light", Source = AppearanceManager.LightThemeSource});

            // add additional themes
            Themes.Add(new Link
            {
                DisplayName = "bing image",
                Source = new Uri("/Active Dictionary;component/Assets/ModernUI.BingImage.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "hello kitty",
                Source = new Uri("/Active Dictionary;component/Assets/ModernUI.HelloKitty.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "love",
                Source = new Uri("/Active Dictionary;component/Assets/ModernUI.Love.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "snowflakes",
                Source = new Uri("/Active Dictionary;component/Assets/ModernUI.Snowflakes.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "R4",
                Source = new Uri("/Active Dictionary;component/Assets/r4.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "Ahmed El gwaily",
                Source = new Uri("/Active Dictionary;component/Assets/GU.xaml", UriKind.Relative)
            });
            Themes.Add(new Link
            {
                DisplayName = "Osama El sayed",
                Source = new Uri("/Active Dictionary;component/Assets/Osama El sayed.xaml", UriKind.Relative)
            });

            AppearanceManager.Current.ThemeSource = theme.Source;
            SelectedTheme = theme;

            var convertFromString = ColorConverter.ConvertFromString(color);
            if (convertFromString != null)
                SelectedAccentColor = AppearanceManager.Current.AccentColor = (Color) convertFromString;

            AppearanceManager.Current.FontSize = (fontSize == "large") ? FontSize.Large : FontSize.Small;

            SelectedFontSize = (AppearanceManager.Current.FontSize == FontSize.Large) ? FontLarge : FontSmall;

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;

            SyncThemeAndColor();
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            foreach (var themee in Themes)
            {
                if (themee.Source == AppearanceManager.Current.ThemeSource)
                {
                    if (SelectedTheme != themee)
                        SelectedTheme = themee;
                    break;
                }
            }
            //this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            if (AppearanceManager.Current.AccentColor != _selectedAccentColor)
                AppearanceManager.Current.AccentColor = _selectedAccentColor;

            AppearanceManager.Current.FontSize = (SelectedFontSize == "large") ? FontSize.Large : FontSize.Small;

            // to update the radio buttons color on the change mechanism page :*
            var changeMechPanel = ChangeMechanism.GetInstance();
            changeMechPanel.SyncMechanismColors();
            // end update colors.

            Configuration.SaveUserSettings(SelectedTheme, SelectedAccentColor.ToString(), SelectedFontSize,
                Width, Height, Mechanism);
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }
    }
}