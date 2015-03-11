using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Active_Dictionary.Classes;
using FirstFloor.ModernUI.Windows.Controls;

namespace Active_Dictionary.Pages
{
    /// <summary>
    ///     Interaction logic for translator.xaml
    /// </summary>
    internal partial class Translator
    {
        private static string _txt;

        public Translator()
        {
            NewTextSent += translator_NewTextSent;

            InitializeComponent();

            ComboFrom.ItemsSource = Models.Translator.Languages.ToArray();
            ComboTo.ItemsSource = Models.Translator.Languages.ToArray();

            ComboFrom.SelectedItem = "English";
            ComboTo.SelectedItem = "Arabic";
            TxtSource.Text = ClipboardText;
            btnTranslate_Click(null, null);
        }

        public static string ClipboardText
        {
            private get { return _txt; }
            set
            {
                if (_txt == value) return;
                _txt = value;
                var trans = new ItemTransferEventArgs {NewText = _txt};

                if (NewTextSent != null)
                    NewTextSent(null, trans);
            }
        }

        private void translator_NewTextSent(object sender, ItemTransferEventArgs e)
        {
            ClearAll();
            TxtSource.Text = e.NewText;
            btnTranslate_Click(null, null);
        }

        private void ClearAll()
        {
            TxtSource.Text = null;
            TxtTarget.Text = null;
            EditReverseTranslation.Text = null;
        }

        private async void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtSource.Text)) return;
            TranslateUnit.Effect = new BlurEffect();
            var isTextLatin = Inteligence.IsTheTextLatin(TxtSource.Text) ||
                              (TxtSource.Text.Contains("e") || TxtSource.Text.Contains("a") ||
                               TxtSource.Text.Contains("o") ||
                               TxtSource.Text.Contains("y") || TxtSource.Text.Contains("b"));
            // Initialize the translator
            var t = new Models.Translator
            {
                SourceLanguage = isTextLatin ? (string) ComboFrom.SelectedItem : (string) ComboTo.SelectedItem,
                TargetLanguage = isTextLatin ? (string) ComboTo.SelectedItem : (string) ComboFrom.SelectedItem,
                SourceText = TxtSource.Text
            };

            TxtTarget.Text = string.Empty;

            EditReverseTranslation.Text = string.Empty;


            // Translate the text
            try
            {
                // Forward translation
                Cursor = Cursors.Wait;
                ProgressWait.Visibility = Visibility.Visible;
                //_lblStatus.Text = "Translating...";
                await t.TranslateAsync();

                TxtTarget.Text = t.Translation;
                // Reverse translation
                //_lblStatus.Text = "Reverse translating...";

                //Thread.Sleep(500); // let Google breathe
                t.SourceLanguage = (string) ComboTo.SelectedItem;
                t.TargetLanguage = (string) ComboFrom.SelectedItem;
                t.SourceText = TxtTarget.Text;
                if (!await t.TranslateAsync())
                {
                    const MessageBoxButton btn = MessageBoxButton.OK;
                    ModernDialog.ShowMessage("حدث خطأ أثناء محاولة الترجمه فلتتفحص وصلة الانترنت خاصتك",
                        "أسف جداً لقد خذلتك",
                        btn);
                    return;
                }
                EditReverseTranslation.Text = t.Translation;
            }
            catch
            {
            }
            finally
            {
                //_lblStatus.Text = string.Empty;
                ProgressWait.Visibility = Visibility.Hidden;
                Cursor = Cursors.Arrow;
                TranslateUnit.Effect = null;
                BtnSpeak_OnClick(null, null);
            }
        }

        private static event EventHandler<ItemTransferEventArgs> NewTextSent;

        private void BtnSpeak_OnClick(object sender, RoutedEventArgs e)
        {
            var mark = Speaker.Current;
            try
            {
                mark.Say(EditReverseTranslation.Text);
            }
            catch
            {
            }
        }
    }
}