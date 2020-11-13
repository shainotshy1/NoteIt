using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NoteIt.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Recording";
            IsRecording = "Not Recording:(";
            ImageSource = "recording.png";

            Recording = new Command(() =>
            {
                if (IsRecording == "Not Recording:(")
                {
                    IsRecording = "Recording!";
                    ImageSource = "recording2.png";
                }

                else
                {
                    IsRecording = "Not Recording:(";
                    ImageSource = "recording.png";
                }
            });
        }

        public Command Recording { get; }

    }
}