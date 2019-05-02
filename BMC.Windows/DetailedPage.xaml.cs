using BMC.Common;
using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BMC
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailedPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public BMCInfo info { get; set; }
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public DetailedPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            MyBar.Visibility = Visibility.Collapsed;
           
            if (e.Parameter != null && !string.IsNullOrWhiteSpace(e.Parameter.ToString()))
            {
                info = e.Parameter as BMCInfo;
                pageTitle.Text = info.Name;
                BitmapImage bi = new BitmapImage(new Uri("ms-appx:///" + info.MyImage));
                Myimage.Source = bi;
                Mytext.Text = info.MyText;
                if (info.YoutubeLink != null)
                {
                    YouTubePlayerMediaElement.Visibility = Visibility.Visible;
                    ProgressProgressBar.Visibility = Visibility.Visible;
                    if (InternetConnection.IsConnectedToInternet())
                    {
                        youtubevideo();
                    }
                    else
                    {
                        ProgressProgressBar.Visibility = Visibility.Collapsed;
                        MessageDialog msg = new MessageDialog("We Cannot Display the video, Please Check your Internet Connection");
                        await msg.ShowAsync();
                    }
                    MyBar.Visibility = Visibility.Visible;
                }
                else {
                    YouTubePlayerMediaElement.Visibility = Visibility.Collapsed;
                    ProgressProgressBar.Visibility = Visibility.Collapsed;
                }


            }
            navigationHelper.OnNavigatedTo(e);
        }

        protected async void youtubevideo()
        {


            try
            {
                ProgressProgressBar.IsEnabled = true;
                var uri = await YouTube.GetVideoUriAsync("QoAOzMTLP5s", YouTubeQuality.Quality720P);



                // “JPF_iD1IXyc” video id from youtube video link
                if (uri != null)
                {

                    YouTubePlayerMediaElement.Source = uri.Uri;
                    YouTubePlayerMediaElement.Play();
                }
                else
                {
                    Debugger.Break();
                    // TODO: Show error message (no video uri found)
                    ProgressProgressBar.IsEnabled = false;
                }
            }
            catch (Exception exception)
            {
                // TODO: Add exception handling
                //Debugger.Break();
                ProgressProgressBar.IsEnabled = false;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

       

        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            ProgressProgressBar.IsEnabled = false;
            ProgressProgressBar.Visibility = Visibility.Collapsed;
        }

        private void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ProgressProgressBar.IsEnabled = false;

        }

        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            YouTubePlayerMediaElement.Position -= TimeSpan.FromMilliseconds(800);

        }

      

        private void Seekbtn_Click(object sender, RoutedEventArgs e)
        {
            YouTubePlayerMediaElement.Position += TimeSpan.FromMilliseconds(800);

        }

        private void YouTubePlayer_DoubleTapped(object sender, RoutedEventArgs e)
        {
            if (YouTubePlayerMediaElement.IsFullWindow == true)
            {
                YouTubePlayerMediaElement.IsFullWindow = false;
                Expand.Icon = new SymbolIcon(Symbol.FullScreen);
            }
            else
            {
                YouTubePlayerMediaElement.IsFullWindow = true;
                Expand.Icon = new SymbolIcon(Symbol.BackToWindow);

            }
        }

        private void Pausebtn_Click(object sender, RoutedEventArgs e)
        {
            if (YouTubePlayerMediaElement.CurrentState == MediaElementState.Playing)
            {
                YouTubePlayerMediaElement.Pause();
                Pausebtn.Icon = new SymbolIcon(Symbol.Play);

            }
            else if (YouTubePlayerMediaElement.CurrentState == MediaElementState.Paused)
            {
                Pausebtn.Icon = new SymbolIcon(Symbol.Pause);
                YouTubePlayerMediaElement.Play();
            }
        }
        bool shape = false;

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (shape == false)
            {
                MyButo0n.Icon = new SymbolIcon(Symbol.Pause);
                string text = Mytext.Text.ToString();
                SpeechSynthesizer s = new SpeechSynthesizer();
                var r = await s.SynthesizeTextToStreamAsync(text.ToString());
                MyMedia.SetSource(r, r.ContentType);

                MyMedia.Play();
                shape = true;
            }
            else
            {
                MyButo0n.Icon = new SymbolIcon(Symbol.Play);

                MyMedia.Stop();
                shape = false;
            }

        }
    }
}
