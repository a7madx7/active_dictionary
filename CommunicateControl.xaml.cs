using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Active_Dictionary
{
    /// <summary>
    ///     Interaction logic for CommunicateControl.xaml
    /// </summary>
    public partial class CommunicateControl : UserControl
    {
        private static string _fbLink = "http://www.facebook.com/Ph.Ahmed.Hamdy.Emara";
        private static string _twitterLink = "https://twitter.com/khaterahObj";
        private static string _gpLink = "http://plus.google.com/u/0/105225295166930709817";
        private static string _youtubeLink = "http://www.youtube.com/user/khaterahObj";
        private static string _websiteLink = "http://khaterah.com";
        private static string _fbLink2;
        private static string _twitterLink2;
        private static string _gpLink2;
        private static string _youtubeLink2;
        private static string _websiteLink2;

        public CommunicateControl()
        {
            InitializeComponent();
        }

        private void SetupLinks()
        {
            switch (Name)
            {
                case "communicateWithGU":
                    _fbLink = "http://www.facebook.com/ahmed.elgewaily";
                    _twitterLink = "https://twitter.com/Ahmed_ElGewaily";
                    _gpLink = "https://plus.google.com/113879838125115495796/posts";
                    _youtubeLink = "http://www.youtube.com/user/augpharma";
                    _websiteLink = "http://www.learnpharmacyathome.wordpress.com";

                    _fbLink2 = "https://www.facebook.com/pages/First-Pharmacology-Course-Online-FPCO/180463351995781";
                    _twitterLink2 = null;
                    _gpLink2 = null;
                    _youtubeLink2 = "http://www.youtube.com/user/augpharma7";
                    _websiteLink2 = null;
                    break;

                default:
                    _fbLink = "http://www.facebook.com/Ph.Ahmed.Hamdy.Emara";
                    _twitterLink = "https://twitter.com/khaterahObj";
                    _gpLink = "http://plus.google.com/u/0/105225295166930709817";
                    _youtubeLink = "http://www.youtube.com/user/khaterahObj";
                    _websiteLink = "http://khaterah.com";

                    _fbLink2 = "http://www.fb.com/khaterah.official";
                    _twitterLink2 = null;
                    _gpLink2 = "https://plus.google.com/b/115081285226969045312/";
                    _youtubeLink2 = null;
                    _websiteLink2 = null;
                    break;
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            SetupLinks();
            var button = sender as Button;
            if (button == null) return;
            switch (button.Name)
            {
                case "btnFB":
                    Process.Start(_fbLink);
                    if (!string.IsNullOrEmpty(_fbLink2)) Process.Start(_fbLink2);
                    break;
                case "btnTwitter":
                    Process.Start(_twitterLink);
                    if (!string.IsNullOrEmpty(_twitterLink2)) Process.Start(_twitterLink2);
                    break;
                case "btnYoutube":
                    Process.Start(_youtubeLink);
                    if (!string.IsNullOrEmpty(_youtubeLink2)) Process.Start(_youtubeLink2);
                    break;
                case "btnGP":
                    Process.Start(_gpLink);
                    if (!string.IsNullOrEmpty(_gpLink2)) Process.Start(_gpLink2);
                    break;
                case "btnWebsite":
                    Process.Start(_websiteLink);
                    if (!string.IsNullOrEmpty(_websiteLink2)) Process.Start(_websiteLink2);
                    break;
            }
        }
    }
}