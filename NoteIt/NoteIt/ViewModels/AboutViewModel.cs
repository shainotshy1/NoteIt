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
            NoteLength = 0;

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

                    if (Notes.Count % 14 == 0 && NoteLength > 0)
                    {
                        Notes.Clear();
                        for (int i = NoteLength - NoteLength % 14; i < NoteLength; i++)
                        {
                            Notes.Add(NotesCache[i]);
                        }
                    }
                    NoteCount++;
                    NoteLength++;

                    string shiftedId;

                    if (NoteCount < 10)
                    {
                        shiftedId = Convert.ToString(NoteCount) + "    ";

                    }
                    else if (NoteCount < 100)
                    {
                        shiftedId = Convert.ToString(NoteCount) + "  ";
                    }
                    else
                    {
                        shiftedId = Convert.ToString(NoteCount);
                    }

                    TextFormat(TextNote, shiftedId);

                    isTranscribing = false;
                    IsRecording = "";
                    ImageSource = "recording.png";
                    TextNote = "";
                }

            });

            NewNote = new Command(() =>
            {
                if (Notes.Count % 14 == 0 && NoteLength > 0)
                {
                    Notes.Clear();
                    for (int i = NoteLength - NoteLength % 14; i < NoteLength; i++)
                    {
                        Notes.Add(NotesCache[i]);
                    }
                }
                NoteCount++;
                NoteLength++;

                string shiftedId;

                if (NoteCount < 10)
                {
                    shiftedId = Convert.ToString(NoteCount) + "    ";
                    
                }
                else if (NoteCount < 100)
                {
                    shiftedId = Convert.ToString(NoteCount) + "  ";
                }
                else
                {
                    shiftedId = Convert.ToString(NoteCount);
                }
                //fix to make length of 30 then split

                string text = "This is a sample text that is very extra extra very very very long for testing purposes";

                TextFormat(text, shiftedId);
            });
            
            Clear = new Command(() =>
            {
                Notes.Clear();
                NotesCache.Clear();
                NoteCount = 0;
                NoteLength = 0;
            });

            Forward = new Command(() =>
            {
                if (Notes.Count > 0)
                {
                    int currentMaxIndex = Notes[0].Id +13;
                    int dif = NoteLength - currentMaxIndex;
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

        void TextFormat(string text,string shiftedId) 
        {
            string cutText;

            if (text.Length > 38)
            {
                var recipe = new Note { Id = NoteLength, Text = text.Substring(0, 38), ShiftedId = shiftedId };
                Notes.Add(recipe);
                NotesCache.Add(recipe);

                for (int i = 38; i < text.Length; i += 38)
                {
                    NoteLength++;

                    if (Notes.Count % 14 == 0 && NoteLength > 0)
                    {
                        Notes.Clear();
                        for (int j = NoteLength - NoteLength % 14; j < NoteLength - 1; j++)
                        {
                            Notes.Add(NotesCache[j]);
                        }
                    }

                    if (i + 38 >= text.Length)
                    {
                        recipe = new Note { Id = NoteLength, Text = text.Substring(i), ShiftedId = "       " };
                        Notes.Add(recipe);
                        NotesCache.Add(recipe);
                        break;
                    }
                    else
                    {
                        cutText = text.Substring(i, 38);

                        recipe = new Note { Id = NoteLength, Text = cutText, ShiftedId = "       " };
                        Notes.Add(recipe);
                        NotesCache.Add(recipe);
                    }
                }
            }
            else
            {
                if (Notes.Count % 14 == 0 && NoteLength > 0)
                {
                    Notes.Clear();
                    for (int i = NoteLength - NoteLength % 14; i < NoteLength; i++)
                    {
                        Notes.Add(NotesCache[i]);
                    }
                }

                NoteLength++;
                var recipe = new Note { Id = NoteLength, Text = text, ShiftedId = shiftedId };
                Notes.Add(recipe);
                NotesCache.Add(recipe);
            }
        }
    }
}