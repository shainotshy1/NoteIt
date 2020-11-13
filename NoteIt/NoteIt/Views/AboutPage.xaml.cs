using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteIt.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void recordingButton_Clicked(object sender, EventArgs e)
        {
            await recordingButton.ScaleTo(.4, 250, Easing.BounceIn);
            await recordingButton.ScaleTo(.3, 250, Easing.BounceOut);
        }
    }
}