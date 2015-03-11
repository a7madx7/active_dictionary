using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using Active_Dictionary.Classes;
using Active_Dictionary.Content;
using Active_Dictionary.Pages;
using Active_Dictionary.ViewModels;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Translator = Active_Dictionary.Pages.Translator;

namespace Active_Dictionary
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static string _color;
        private static string _fontSize;
        private static string _mech;
        internal static bool IsFirstTimeUse;

        private readonly DispatcherTimer _resizeTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 1500),
            IsEnabled = false
        };

        public MainWindow()
        {
            InitializeComponent();


            _resizeTimer.Tick += _resizeTimer_Tick;

            try
            {
                LoadUserSettings();
            }
            catch
            {
            }
        }

        private string CurrentHeight
        {
            get { return Height.ToString(CultureInfo.InvariantCulture); }
            set
            {
                var newVal = value;
                if (string.IsNullOrEmpty(newVal))
                {
                    Height = 700;
                    return;
                }

                Height = double.Parse(newVal);
            }
        }

        private string CurrentWidth
        {
            get { return Width.ToString(CultureInfo.InvariantCulture); }
            set
            {
                var newVal = value;
                if (string.IsNullOrEmpty(newVal))
                {
                    Width = 500;
                    return;
                }

                Width = double.Parse(newVal);
            }
        }

        private void LoadUserSettings()
        {
            var persona = Configuration.LoadUserSettings();
            var themUri = persona[0];
            var loadedTheme = new Link {Source = new Uri(themUri, UriKind.Relative)};

            try
            {
                _color = persona[1];
            }
            catch
            {
            }

            try
            {
                _fontSize = persona[2];
            }
            catch
            {
            }

            try
            {
                CurrentWidth = persona[3];
                SettingsAppearanceViewModel.Width = CurrentWidth ?? Width.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
            }

            try
            {
                CurrentHeight = persona[4];
                SettingsAppearanceViewModel.Height = CurrentHeight ?? Height.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
            }
            try
            {
                _mech = persona[5];
                DBDataGetter.Mechanism =
                    SettingsAppearanceViewModel.Mechanism = string.IsNullOrEmpty(_mech) ? "after" : _mech;
            }
            catch
            {
            }

// ReSharper disable once ObjectCreationAsStatement
            new SettingsAppearanceViewModel(loadedTheme, _color, _fontSize, CurrentWidth, CurrentHeight,
                DBDataGetter.Mechanism);

            try
            {
                Width = Convert.ToDouble(CurrentWidth);
                Height = Convert.ToDouble(CurrentHeight);
            }
            catch
            {
            }
        }

        private void ModernWindow_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            _resizeTimer.IsEnabled = true;
            _resizeTimer.Stop();
            _resizeTimer.Start();
        }

        private void _resizeTimer_Tick(object sender, EventArgs e)
        {
            _resizeTimer.IsEnabled = false;
            SettingsAppearanceViewModel.Width = Width.ToString(CultureInfo.InvariantCulture);
            SettingsAppearanceViewModel.Height = Height.ToString(CultureInfo.InvariantCulture);

            Configuration.SaveUserSettings(SettingsAppearanceViewModel.StaticSelectedTheme,
                SettingsAppearanceViewModel.StaticSelectedColor.ToString(),
                SettingsAppearanceViewModel.StaticSelectedFont, Width.ToString(CultureInfo.InvariantCulture),
                Height.ToString(CultureInfo.InvariantCulture), SettingsAppearanceViewModel.Mechanism);
        }

        private async void ModernWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            ShowWelcomeIfFirstTimeRunning();
            if (Height == 700) Height = 768;

            if (Clipboard.ContainsText())
            {
                Home.ClipboardText = Clipboard.GetText().Trim();
            }

            await Updater.CheckForUpdateAsync();
        }

        private static void ShowWelcomeIfFirstTimeRunning()
        {
            if (!IsFirstTimeUse) return;

            ShowMessage("", "");
            var msg = new StringBuilder();
            msg.Append("إذا أردت القاموس أن ينطق لك كلمه اضغط على انتر,,");
            msg.Append(Environment.NewLine);
            msg.Append("اذا أردته أن يقوم بحذف النص الموجود في مربع البحث اضغط سكيب,,");
            msg.Append(Environment.NewLine);
            msg.Append(Environment.NewLine);
            msg.Append("إذا أردت أن تعرف معنى كلمه وكان القاموس مغلقاً كل ما عليك فعله هو تحديدها ثم نسخها ثم الضغط على");
            msg.Append(Environment.NewLine);
            msg.Append("ALT + CTRL + A");
            msg.Append(Environment.NewLine);
            msg.Append(
                "وهو سيقوم بفتحها لك مباشرة فلو كانت كلمه واحده سوف يقوم بترجمتها ولو كانت مقاله سوف يمررها لمترجم جوجل وسوف يقوم بترجمتها كاملة لك ومن ثم نطقها ولو كانت عنوان ويب سوف يقوم بفتحه لك في المتصفح أيضاَ,,, ونعم يوجد في برنامج القاموس النشط متصفح كامل في حالة لو احتجت زيارة جوجل أو ويكيبيديا ");
            msg.Append(Environment.NewLine);
            msg.Append(Environment.NewLine);
            msg.Append(
                "هذا البرنامج مصمم من صيدلي لأخوانه الصيادله فهو مصمم كي يفهمك ويريحك وهو ليس للطالب فقط بل للصيدلي العامل أيضاً وللباحثين");
            msg.Append(Environment.NewLine);
            msg.Append("إن أعجبك هذا المشروع لا تبخل علينا بدعمه عن طريق نشره ومتابعتنا عبر مواقع التواصل الاجتماعي");
            msg.Append(Environment.NewLine);
            msg.Append("احرص على أخذ جوله في قسم(عن البرنامج) للتعرف علينا");
            msg.Append(Environment.NewLine);
            msg.Append("استمتع بأفضل قاموس طبي حياتي معاصر تم تصميمه حتى هذه اللحظه");

            const string title = "نصائح سريعه";
            ShowMessage(msg.ToString(), title);
        }

        private static void ShowMessage(string msg = "", string title = "")
        {
            var pl = new WrapPanel();
            if (string.IsNullOrEmpty(msg))
                msg =
                    " مرحبا بكم في أفضل قاموس موجود في السوق مقدم لكم من مؤسسة خاطره مجاناً وبدون أي كلفه,, هذا التطبيق هديه خاصه لمتابعي د. أحمد الجويلي حفظه الله لنا وأكثر من أمثاله";
            if (string.IsNullOrEmpty(title))
                title = "مرحباً بك يا صديقي";

            pl.Children.Add(new TextBlock
            {
                Text = msg,
                TextWrapping = TextWrapping.Wrap
            });
            var page = new ScrollViewer {Content = pl};

            new ModernDialog
            {
                Title = title,
                FlowDirection = FlowDirection.RightToLeft,
                Topmost = true,
                Effect = new DropShadowEffect(),
                FontSize = 28,
                Content = page
            }.ShowDialog();
        }

        private void ModernWindow_Activated_1(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                var newText = Clipboard.GetText().Trim();
                // if url show the web
                if (Inteligence.IsTheTextUrl(newText))
                {
                    Dispatcher.Invoke(delegate
                    {
                        var frame = (ModernFrame) GetTemplateChild("ContentFrame");
                        NavigationCommands.GoToPage.Execute("/Pages/browser.xaml", frame);
                        Browser.ClipboardText = newText;
                    });
                    return;
                }
                // if long text show the google translator
                if (Inteligence.IsTheTextLong(newText))
                {
                    // this is the test string
                    Dispatcher.Invoke(delegate
                    {
                        var frame = (ModernFrame) GetTemplateChild("ContentFrame");
                        NavigationCommands.GoToPage.Execute("/Pages/translator.xaml", frame);
                        Translator.ClipboardText = newText;

                        // TODO: ADD ABSTRACTION LAYER FOR SENDING TEXT MECHANISM
                    });
                    return;
                }
                Home.ClipboardText = newText;
            }
        }
    }
}