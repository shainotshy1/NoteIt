using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NoteIt.ViewModels
{
    public class SettingsViewModel: BaseViewModel
    {
        public SettingsViewModel()
        {
            Title = "Settings";

            BackCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//AboutPage");
            });
        }

        public Command BackCommand { get; }
    }
}
