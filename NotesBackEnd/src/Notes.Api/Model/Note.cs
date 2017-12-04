using System;

namespace Notes.Api.Model
{
    public class Note
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
        public bool IsEditActive { get; set; }

        public Note(string text, Guid id, bool isEditActive)
        {
            Text = text;
            Id = id;
            IsEditActive = isEditActive;
        }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Id)}: {Id}, {nameof(IsEditActive)}: {IsEditActive}";
        }
    }
}