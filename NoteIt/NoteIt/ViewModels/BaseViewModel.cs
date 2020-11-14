using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using NoteIt.Models;
using NoteIt.Services;

namespace NoteIt.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string isRecording = string.Empty;
        public string IsRecording 
        {
            get { return isRecording; }
            set { SetProperty(ref isRecording, value); } 
        }
        string imageSource = string.Empty;
        public string ImageSource
        {
            get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
        }
        int noteCount = 0;
        public int NoteCount 
        { 
            get { return noteCount; }
            set { SetProperty(ref noteCount, value); } 
        }

        int noteLength = 0;
        public int NoteLength
        {
            get { return noteLength; }
            set { SetProperty(ref noteLength, value); }
        }
        
        string textNote = string.Empty;
        public string TextNote
        {
            get { return textNote; }
            set { SetProperty(ref textNote, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
