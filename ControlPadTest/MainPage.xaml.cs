using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ControlPadTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            CoreWindow.GetForCurrentThread().KeyDown += MainPage_KeyDown;
        }

        private void MainPage_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            labelTextBlock.Text = String.Format("Key/Button Event: {0}", args.VirtualKey.ToString());

            //This plays audio converted from text, but current Dev Kit doesn't have the media components access
            //TextToSpeech(args.VirtualKey.ToString());
        }

        //This plays audio converted from text, but current Dev Kit doesn't have the media components access
        private async void TextToSpeech(string text)
        {
            try
            {
                var speechSynthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                SpeechSynthesisStream synthesisStream = await speechSynthesizer.SynthesizeTextToStreamAsync(text);
                MediaElement media = new MediaElement();
                media.SetSource(synthesisStream, synthesisStream.ContentType);
                media.Play();
            }
            catch(Exception ex)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Unable to synthesize text: " + ex.Message);
                await messageDialog.ShowAsync();

            }
        }
    }
}
