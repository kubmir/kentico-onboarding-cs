using System;

namespace Notes.Models.Model
{
    public class Note
    {
        public string Text { get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Id)}: {Id}";
        }
    }
}