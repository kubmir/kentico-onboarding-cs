using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace Notes.Api.Tests.Utils
{
    static class EqualityConstraintExtension
    {
        public static EqualConstraint UsingNoteComparer(this EqualConstraint equalConstraint) 
            => equalConstraint.Using(NoteEqualityComparer.Instance);
    }
}
