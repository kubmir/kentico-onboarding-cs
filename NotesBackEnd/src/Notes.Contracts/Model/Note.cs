using System;

namespace Notes.Contracts.Model
{
    public class Note
    {
        public String Text { get; set; }
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public override String ToString()
            => $"{nameof(Text)}: {Text}, {nameof(Id)}: {Id}, " +
               $"{nameof(CreationDate)}: {CreationDate}, " +
               $"{nameof(LastModificationDate)}: {LastModificationDate}";
    }
}