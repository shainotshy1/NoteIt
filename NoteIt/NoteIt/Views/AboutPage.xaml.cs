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
            await recordingButton.ScaleTo(.57, 180, Easing.BounceIn);
            await recordingButton.ScaleTo(.5, 180, Easing.BounceOut);
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (sender == backward)
            {
                await backward.ScaleTo(0.75, 180, Easing.BounceIn);
                await backward.ScaleTo(0.7, 180, Easing.BounceIn);
            }
            else
            {
                await forward.ScaleTo(0.75, 180, Easing.BounceIn);
                await forward.ScaleTo(0.7, 180, Easing.BounceIn);
            }
        }


    }
}