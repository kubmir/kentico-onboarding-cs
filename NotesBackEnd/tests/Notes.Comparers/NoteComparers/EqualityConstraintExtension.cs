using NUnit.Framework.Constraints;

namespace Notes.Comparers.NoteComparers
{
    public static class EqualityConstraintExtension
    {
        public static EqualConstraint UsingNoteComparer(this EqualConstraint equalConstraint) 
            => equalConstraint.Using(NoteEqualityComparer.Instance);
    }
}
