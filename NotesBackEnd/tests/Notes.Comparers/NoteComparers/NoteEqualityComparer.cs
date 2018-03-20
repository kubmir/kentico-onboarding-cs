using System;
using System.Collections.Generic;
using Notes.Contracts.Model;

namespace Notes.Comparers.NoteComparers
{
    internal sealed class NoteEqualityComparer : IEqualityComparer<Note>
    {
        private static readonly Lazy<NoteEqualityComparer> LazyInstance = new Lazy<NoteEqualityComparer>(() => new NoteEqualityComparer());

        public static NoteEqualityComparer Instance => LazyInstance.Value;

        private NoteEqualityComparer()
        {

        }

        public Boolean Equals(Note x, Note y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, objB: null) || ReferenceEquals(objA: null, objB: y))
            {
                return false;
            }

            return x.Id == y.Id && x.Text == y.Text &&
                   x.CreationDate == y.CreationDate &&
                   x.LastModificationDate == y.LastModificationDate;
        }

        public Int32 GetHashCode(Note obj)
        {
            var hashCodeText = obj.Text == null ? 0 : obj.Text.GetHashCode();
            var hashCodeId = obj.Id.GetHashCode();

            return hashCodeId ^ hashCodeText;
        }
    }
}
