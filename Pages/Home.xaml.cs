using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Active_Dictionary.Classes;
using Active_Dictionary.Content;
using Active_Dictionary.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;

namespace Active_Dictionary.Pages
{
    /// <summary>
    ///     Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private static string _txt;
        private readonly Dictionary<string, string> _resultsInversed = new Dictionary<string, string>();
        private Dictionary<string, string> _results = new Dictionary<string, string>();

        /// <summary>
        ///     the default constructor with event registration in it.
        /// </summary>
        public Home()
        {
            InitializeComponent();
            DBDataGetter.SearchCompleted += DBDataGetter_SearchCompleted;
            NewTextSent += Home_NewTextSent;

            if (txtSearchTerm.Text != null && txtSearchTerm.Text != ClipboardText)
                // no need to fire-up text changed event when no need.
                txtSearchTerm.Text = ClipboardText;
        }

        public static string ClipboardText
        {
            private get { return _txt; }
            set
            {
                if (_txt == value) return;
                _txt = value;
                var trans = new ItemTransferEventArgs {NewText = value};

                if (NewTextSent != null)
                    NewTextSent(null, trans);
            }
        }

        private void Home_NewTextSent(object sender, ItemTransferEventArgs e)
        {
            try
            {
                ClearAll();
                txtSearchTerm.Text = e.NewText;
            }
            catch
            {
            }
        }

        /// <summary>
        ///     Clears every thing for a new search.
        /// </summary>
        private void ClearAll()
        {
            lstResults.ItemsSource = null;
            txtSearchTerm.Text = null;
            lblTranslation.Text = null;
            txtSearchTerm.Focus();
        }

        private static event EventHandler<ItemTransferEventArgs> NewTextSent;

        private void DBDataGetter_SearchCompleted(object sender, SearchEventArgs e)
        {
            // there was a bug that the application wouldn't display the correct search results if the user writes too fast
            // and that's because the search function runs asynchronously so we had to check if the user wrote another letters and if so we invoke
            // a new search for that string, if you want to understand that bug try to remove the following code and run InvokeNewSearch().

            // the try catch close is mainly added to enhance the speed of the search id needed, not for errors.
            try
            {
                if (e.KeyLength < txtSearchTerm.Text.Length)
                {
                    InvokeNewSearch();
                }
                else
                {
                    // if the search done correctly display the results finally, not before that.
                    _results = e.Results;
                    if (Inteligence.IsTheTextLatin(txtSearchTerm.Text))
                        lstResults.ItemsSource = _results.Keys;
                    else
                    {
                        foreach (var result in _results)
                        {
                            var tempVal = result.Key;
                            var tempKey = result.Value;

                            if (!_resultsInversed.Keys.Contains(tempKey))
                                _resultsInversed.Add(tempKey, tempVal);
                        }
                        lstResults.ItemsSource = _results.Values;
                    }
                    if (lstResults.Items.Count > 0)
                        lstResults.SelectedIndex = 0;
                }
            }
            catch
            {
            }
        }

        private void txtSearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            InvokeNewSearch();
            ChangeFlowDirection();
        }

        /// <summary>
        ///     if the user is entering valid english characters set the flow direction to "left to right"
        ///     otherwise set it to "right to left"
        /// </summary>
        private void ChangeFlowDirection()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchTerm.Text))
                {
                    ClearAll();
                    return;
                }
                if (txtSearchTerm.Text.Contains(".") || txtSearchTerm.Text.Contains(",") ||
                    txtSearchTerm.Text.Contains("*") || txtSearchTerm.Text.Contains("-") ||
                    txtSearchTerm.Text.Contains("/") || txtSearchTerm.Text.Contains("_")
                    || txtSearchTerm.Text.Contains(" ")) return;

                lstResults.FlowDirection = lblTranslation.FlowDirection = txtSearchTerm.FlowDirection =
                    Inteligence.IsTheTextLatin(txtSearchTerm.Text)
                        ? FlowDirection.LeftToRight
                        : FlowDirection.RightToLeft;
            }
            catch
            {
            }
        }

        /// <summary>
        ///     invokes a new search request if the search term is not null.
        /// </summary>
        private void InvokeNewSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchTerm.Text))
                {
                    if (_results.Count > 0)
                    {
                        _results.Clear();
                    }
                    DBDataGetter.GetTranslationAsync(txtSearchTerm.Text);
                }
                else // if no text in the txtSearchTerm TextBox
                {
                    lblTranslation.Text = "";
                }
            }
            catch
            {
            }
        }

        private void lstResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lblTranslation.Foreground = new SolidColorBrush(SettingsAppearanceViewModel.StaticSelectedColor);
                if (Inteligence.IsTheTextLatin(txtSearchTerm.Text))
                    lblTranslation.Text = (lstResults.SelectedValue != null)
                        ? _results[lstResults.SelectedValue.ToString()]
                        : "";
                else
                    lblTranslation.Text = (lstResults.SelectedValue != null)
                        ? _resultsInversed[lstResults.SelectedValue.ToString()]
                        : "";
                //lblTranslation.FontSize = (double) ((lblTranslation.Text.Length) * (2*lblTranslation.Text.Length));
            }
            catch
            {
            }
        }

        /// <summary>
        ///     to be able to navigate through the list using up and down keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Down:
                        lstResults.SelectedIndex = lstResults.SelectedItem != null ? ++lstResults.SelectedIndex : 0;
                        if (lstResults.SelectedItem != null) lstResults.ScrollIntoView(lstResults.SelectedItem);
                        break;
                    case Key.Up:
                        if (lstResults.SelectedIndex == 0) return;
                        lstResults.SelectedIndex--;
                        if (lstResults.SelectedItem != null) lstResults.ScrollIntoView(lstResults.SelectedItem);
                        break;
                }
            }
            catch
            {
            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NewTextSent == null)
                NewTextSent += Home_NewTextSent;

            txtSearchTerm.Focus();
        }

        private void BtnSpeak_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mark = Speaker.Current;
                mark.Say(lstResults.SelectedItem.ToString());
            }
            catch
            {
            }
        }

        private void lstResults_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnSpeak_OnClick(null, null);
            var pl = new WrapPanel();

            pl.Children.Add(new TextBlock
            {
                Text = lblTranslation.Text,
                TextWrapping = TextWrapping.Wrap
            });

            var page = new ScrollViewer {Content = pl};
            new ModernDialog
            {
                Title = lstResults.SelectedItem.ToString(),
                FlowDirection = FlowDirection.RightToLeft,
                Topmost = true,
                Effect = new DropShadowEffect(),
                FontSize = 28,
                Content = page
            }.ShowDialog();
        }
    }

    internal class ItemTransferEventArgs
    {
        internal string NewText;
    }
}