using Microsoft.CognitiveServices.Speech;
using NoteIt.Models;
using NoteIt.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NoteIt.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        
        public AboutViewModel()
        {
            TextNote = "";

            micService = DependencyService.Resolve<IMicrophoneService>();

            Title = "Recording";
            IsRecording = "";
            ImageSource = "recording.png";

            Notes = new ObservableCollection<Note>();
            NotesCache = new ObservableCollection<Note>();
            NoteCount = 0;

            Recording = new Command(async () =>
            {
                if (recognizer == null)
                {
                    var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
                    recognizer = new SpeechRecognizer(config);
                    recognizer.Recognized += (obj, args) =>
                    {
                        UpdateTranscription(args.Result.Text);
                    };
                }


                if (IsRecording == "")
                {
                    IsRecording = "Recording!";
                    ImageSource = "recording2.png";

                    try
                    {
                        await recognizer.StartContinuousRecognitionAsync();
                    }
                    catch (Exception ex)
                    {
                        UpdateTranscription(ex.Message);
                    }
                }

                else
                {
                    try
                    {
                        await recognizer.StopContinuousRecognitionAsync();
                    }
                    catch (Exception ex)
                    {
                        UpdateTranscription(ex.Message);
                    }

                    if (Notes.Count % 14 == 0 && NoteCount > 0)
                    {
                        Notes.Clear();
                        for (int i = NoteCount - NoteCount % 14; i < NoteCount; i++)
                        {
                            Notes.Add(NotesCache[i]);
                        }
                    }

                    NoteCount++;

                    var recipe = new Note { Id = NoteCount, Text = TextNote };
                    Notes.Add(recipe);
                    NotesCache.Add(recipe);

                    isTranscribing = false;
                    IsRecording = "";
                    ImageSource = "recording.png";
                    TextNote = "";
                }

            });

            NewNote = new Command(() =>
            {
                if (Notes.Count % 14 == 0 && NoteCount>0)
                {
                    Notes.Clear();
                    for (int i = NoteCount - NoteCount % 14; i < NoteCount; i++)
                    {
                        Notes.Add(NotesCache[i]);
                    }
                }

                NoteCount++;

                var recipe = new Note { Id = NoteCount, Text = "This is a sample text" };
                Notes.Add(recipe);
                NotesCache.Add(recipe);
            });
            
            Clear = new Command(() =>
            {
                Notes.Clear();
                NotesCache.Clear();
                NoteCount = 0;
            });

            Forward = new Command(() =>
            {
                if (Notes.Count > 0)
                {
                    int currentMaxIndex = Notes[0].Id +13;
                    int dif = NoteCount - currentMaxIndex;
                    if (Notes.Count == 14 && dif > 0)
                    {
                        if (currentMaxIndex >= 14)
                        {
                            Notes.Clear();

                            for (int i = currentMaxIndex; i < currentMaxIndex + 14; i++)
                            {
                                if(NotesCache.Count - 1 < i)
                                {
                                    break;
                                }
                                Notes.Add(NotesCache[i]);
                            }
                        }
                    }
                }
            });

            Backward = new Command(() =>
            {
                if (Notes.Count > 0)
                {
                    int currentMaxIndex = Notes[0].Id;

                    if (currentMaxIndex > 14)
                    {
                        Notes.Clear();

                        int delta = currentMaxIndex % 14;

                        for (int i = currentMaxIndex - 15; i < currentMaxIndex - 1; i++)
                        {
                            Notes.Add(NotesCache[i]);
                        }
                    }
                }
            });
        }

        public Command Recording { get; }
        public Command NewNote { get; }
        public Command Clear { get; }
        public Command Forward { get; }
        public Command Backward { get; }
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Note> NotesCache { get; set; }
        public SpeechRecognizer recognizer { get; set; }
        public IMicrophoneService micService { get; set; }
        public bool isTranscribing { get; set; }
        void UpdateTranscription(string newText)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!string.IsNullOrWhiteSpace(newText))
                {
                    TextNote += $"{newText}";
                }
            });
        }
    }
}