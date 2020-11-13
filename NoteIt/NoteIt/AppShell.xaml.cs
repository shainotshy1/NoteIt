using System;
using System.Collections.Generic;
using NoteIt.ViewModels;
using NoteIt.Views;
using Xamarin.Forms;

namespace NoteIt
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Settings");
            //await Navigation.PushAsync(new Settings());
        }
    }
}
