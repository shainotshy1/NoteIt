using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NoteIt.Services;
using NoteIt.Views;

namespace NoteIt
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
