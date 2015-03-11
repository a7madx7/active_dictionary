using System.Windows.Controls;
using Active_Dictionary.ViewModels;

namespace Active_Dictionary.Content
{
    /// <summary>
    ///     Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class SettingsAppearance : UserControl
    {
        public SettingsAppearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            DataContext = SettingsAppearanceViewModel.GetInstance;
        }
    }
}