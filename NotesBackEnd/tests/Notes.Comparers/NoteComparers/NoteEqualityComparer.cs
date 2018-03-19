﻿using System;
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

            return x.Id == y.Id && x.Text == y.Text &&
                   x.CreationDate == y.CreationDate &&
                   x.LastModificationDate == y.LastModificationDate;
        }

        public int GetHashCode(Note obj)
        {
            int hashCodeText = obj.Text == null ? 0 : obj.Text.GetHashCode();
            int hashCodeId = obj.Id.GetHashCode();

            return hashCodeId ^ hashCodeText;
        }
    }
}