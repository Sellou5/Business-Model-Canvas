using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BMC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondPage : Page
    {
        public SecondPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(DetailedPage));
            //if (rootFrame != null && rootFrame.CanGoBack)
            //{
            //    Frame.Navigate(typeof(DetailedPage));
            //    //rootFrame.GoBack();
            //    e.Handled = true;
            //}
        }

        public BMCInfo Info { get; set; }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        /// 






        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter != null && !string.IsNullOrWhiteSpace(e.Parameter.ToString()))
            {
                MyBar.Visibility = Visibility.Collapsed;
                Info = e.Parameter as BMCInfo;
                MyName.Text = Info.Name;
                MyTexts.Text = Info.MyText;
                if (Info.MyImage != null)
                {
                    BitmapImage bi = new BitmapImage(new Uri("ms-appx:///" + Info.MyImage));
                    MyImaaage.Source = bi;
                }
                if (Info.YoutubeLink != null)
                {
                    MyBar.Visibility = Visibility.Visible;
                    YouTubePlayer.Visibility = Visibility.Visible;
                    Progress.Visibility = Visibility.Visible;
                    if (InternetConnection.IsConnectedToInternet())
                    {
                        youtubeVideo();
                    }
                    else
                    {
                        Progress.Visibility = Visibility.Collapsed;
                        MessageDialog msg = new MessageDialog("We Cannot Display the video, Please check your Internet connection");
                        await msg.ShowAsync();
                    }
                }
                else
                {
                    YouTubePlayer.Visibility = Visibility.Collapsed;
                    Progress.Visibility = Visibility.Collapsed;
                }

            }
        }
        public async void youtubeVideo()
        {
            try
            {
                Progress.IsEnabled = true;
                var uri = await YouTube.GetVideoUriAsync("QoAOzMTLP5s", YouTubeQuality.Quality720P);
                if (uri != null)
                {
                    YouTubePlayer.Source = uri.Uri;
                    YouTubePlayer.Play();
                }
                else
                {
                    Debugger.Break(); // TODO: Show error message (no video uri found)
                    Progress.IsEnabled = false;
                }
            }
            catch (Exception exception)
            {
                // TODO: Add exception handling
                Debugger.Break();

                Progress.IsEnabled = false;
            }
        }
       
       

        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            YouTubePlayer.Position -= TimeSpan.FromMilliseconds(800);
        }

        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            Progress.IsEnabled = false;
            Progress.Visibility = Visibility.Collapsed;
        }

        private void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Progress.IsEnabled = false;

        }

        private void Pausebtn_Click(object sender, RoutedEventArgs e)
        {
            if (YouTubePlayer.CurrentState == MediaElementState.Playing)
            {
                YouTubePlayer.Pause();
                Pausebtn.Icon = new SymbolIcon(Symbol.Play);

            }
            else if (YouTubePlayer.CurrentState == MediaElementState.Paused)
            {
                Pausebtn.Icon = new SymbolIcon(Symbol.Pause);
                YouTubePlayer.Play();
            }
        }

        private void Seekbtn_Click(object sender, RoutedEventArgs e)
        {
            YouTubePlayer.Position += TimeSpan.FromMilliseconds(800);

        }
        bool shape = false;
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(shape==false)
            {
                Mybutto0n.Icon = new SymbolIcon(Symbol.Pause);
                string text = MyTexts.Text.ToString();
                SpeechSynthesizer s = new SpeechSynthesizer();
                var r = await s.SynthesizeTextToStreamAsync(text.ToString());
                MyMedia.SetSource(r, r.ContentType);

                MyMedia.Play();
                shape = true;
            }
            else
            {
                Mybutto0n.Icon = new SymbolIcon(Symbol.Play);
               
                MyMedia.Stop();
                shape = false;
            }

        }

    }

       

       

       
    }

