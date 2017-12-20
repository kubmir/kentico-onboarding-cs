using System;

namespace Notes.Contracts.Model
{
    public class Note
    {
        public string Text { get; set; }
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Id)}: {Id}, " +
                   $"{nameof(CreationDate)}: {CreationDate}, " +
                   $"{nameof(LastModificationDate)}: {LastModificationDate}";
        }
    }
}