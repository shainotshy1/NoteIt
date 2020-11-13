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
            Title = "Note It";
            IsRecording = "Not Recording:(";

            Recording = new Command(() =>
            {
                if (IsRecording == "Not Recording:(")
                    IsRecording = "Recording!";
                else
                    IsRecording = "Not Recording:(";
            });
        }

        public Command Recording { get; }

    }
}