using System;
using System.Collections.Generic;
using System.Text;

namespace NoteIt.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ShiftedId { get; set; }
    }
}
