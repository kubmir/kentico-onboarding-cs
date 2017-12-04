using System.Collections.Generic;
using Notes.Api.Model;

namespace Notes.Api.Tests.Utils
{
    class NoteEqualityComparer : IEqualityComparer<Note>
    {
        public bool Equals(Note x, Note y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(null, y))
            {
                return false;
            }

            return x.Id == y.Id && x.Text == y.Text && x.IsEditActive == y.IsEditActive;
        }

        public int GetHashCode(Note obj)
        {
            int hashCodeText = obj.Text == null ? 0 : obj.Text.GetHashCode();
            int hashCodeId = obj.Id.GetHashCode();

            return hashCodeId ^ hashCodeText;
        }
    }
}
