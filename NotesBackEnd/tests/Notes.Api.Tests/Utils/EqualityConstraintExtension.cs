using NUnit.Framework.Constraints;

namespace Notes.Api.Tests.Utils
{
    internal static class EqualityConstraintExtension
    {
        public static EqualConstraint UsingNoteComparer(this EqualConstraint equalConstraint) 
            => equalConstraint.Using(NoteEqualityComparer.Instance);
    }
}
