using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Active_Dictionary.Classes;
using Active_Dictionary.ViewModels;

namespace Active_Dictionary.Content
{
    /// <summary>
    ///     Interaction logic for About.xaml
    /// </summary>
    public partial class ChangeMechanism
    {
        private static ChangeMechanism _current;

        public ChangeMechanism()
        {
            InitializeComponent();

            DataContext = SettingsAppearanceViewModel.GetInstance;

            SetDefaultSettings();

            _current = this;
        }

        public static ChangeMechanism GetInstance()
        {
            return _current ?? new ChangeMechanism();
        }

        /// <summary>
        ///     Loads the last edited user settings.
        /// </summary>
        private void SetDefaultSettings()
        {
            try
            {
                switch (Configuration.LoadUserSettings()[5])
                {
                    case "after":
                        RadioAfter.IsChecked = true;
                        break;

                    case "before":
                        RadioBefore.IsChecked = true;
                        break;

                    case "any":
                        RadioAny.IsChecked = true;
                        break;

                    default:
                        RadioAfter.IsChecked = true;
                        break;
                }
            }
            catch
            {
            }

            SyncMechanismColors();
        }

        internal void SyncMechanismColors()
        {
            RadioAfter.Foreground = new SolidColorBrush(SettingsAppearanceViewModel.StaticSelectedColor);
            RadioBefore.Foreground = new SolidColorBrush(SettingsAppearanceViewModel.StaticSelectedColor);
            RadioAny.Foreground = new SolidColorBrush(SettingsAppearanceViewModel.StaticSelectedColor);
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentRadio = sender as RadioButton;
                if (currentRadio != null)
                    currentRadio.Foreground = new SolidColorBrush(SettingsAppearanceViewModel.StaticSelectedColor);
                switch (currentRadio.Name)
                {
                    case "RadioAfter":
                        Configuration.SaveUserSettings(SettingsAppearanceViewModel.StaticSelectedTheme,
                            SettingsAppearanceViewModel.StaticSelectedColor.ToString(),
                            SettingsAppearanceViewModel.StaticSelectedFont,
                            SettingsAppearanceViewModel.Width, SettingsAppearanceViewModel.Height, "after");
                        break;

                    case "RadioBefore":
                        Configuration.SaveUserSettings(SettingsAppearanceViewModel.StaticSelectedTheme,
                            SettingsAppearanceViewModel.StaticSelectedColor.ToString(),
                            SettingsAppearanceViewModel.StaticSelectedFont,
                            SettingsAppearanceViewModel.Width, SettingsAppearanceViewModel.Height, "before");
                        break;

                    case "RadioAny":
                        Configuration.SaveUserSettings(SettingsAppearanceViewModel.StaticSelectedTheme,
                            SettingsAppearanceViewModel.StaticSelectedColor.ToString(),
                            SettingsAppearanceViewModel.StaticSelectedFont,
                            SettingsAppearanceViewModel.Width, SettingsAppearanceViewModel.Height, "any");
                        break;
                }
            }
            catch
            {
            }
        }
    }
}