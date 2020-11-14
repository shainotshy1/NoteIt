﻿using NoteIt.Models;
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
            Title = "Recording";
            IsRecording = "Not Recording:(";
            ImageSource = "recording.png";

            Notes = new ObservableCollection<Note>();
            NotesCache = new ObservableCollection<Note>();
            NoteCount = 0;

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

            NewNote = new Command(() =>
            {

                NoteCount++;

                var recipe = new Note { Id = NoteCount, Text = "This is a sample note" };

                if(Notes[Notes.Count-1].Id != NoteCount)
                {
                    /*for (int i = currentMaxIndex; i < currentMaxIndex + 14; i++)
                            {
                                if(NotesCache.Count - 1 < i)
                                {
                                    break;
                                }
                                Notes.Add(NotesCache[i]);
                            }*/
                }
                if (Notes.Count % 14 == 0)
                {
                    Notes.Clear();
                }

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
    }
}