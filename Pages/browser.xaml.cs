using System;
using System.Windows;
using System.Windows.Controls;
using Active_Dictionary.Classes;
using Awesomium.Core;

namespace Active_Dictionary.Pages
{
    /// <summary>
    ///     Interaction logic for browser.xaml
    /// </summary>
    public partial class Browser
    {
        private static string _txt;

        public Browser()
        {
            InitializeComponent();
            NewTextSent += translator_NewTextSent;
            TxtSearch.Text = ClipboardText;
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

        private void web_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            web.LoadingFrameComplete -= web_LoadingFrameComplete;
            if (web.IsLoaded)
            {
                progressWaitForPage.Visibility = Visibility.Hidden;
            }
        }

        private static event EventHandler<ItemTransferEventArgs> NewTextSent;

        private void translator_NewTextSent(object sender, ItemTransferEventArgs e)
        {
            ClearAll();
            TxtSearch.Text = e.NewText;
        }

        private void ClearAll()
        {
            TxtSearch.Text = null;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            web.LoadingFrameComplete += web_LoadingFrameComplete;
            web.LoadingFrame += web_LoadingFrame;
            progressWaitForPage.Visibility = Visibility.Visible;

            if (Inteligence.IsTheTextUrl(TxtSearch.Text))
            {
                web.Source = new Uri(TxtSearch.Text);
            }
            else
            {
                var q = GoogleSearch.Search(TxtSearch.Text);
                web.Source = new Uri(q);
            }
        }

        private void web_LoadingFrame(object sender, LoadingFrameEventArgs e)
        {
            progressWaitForPage.Visibility = Visibility.Visible;
        }

        private void userControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //web.Dispose();
            //WebCore.Shutdown();
        }
    }
}