using System.Collections.Generic;
using GitReleaseNote.Generator.Models;

namespace GitReleaseNote.Generator.Comparers
{
    public class GitTagComparer : IComparer<GitTag>
    {
        public int Compare(GitTag x, GitTag y)
        {
            var prefixComparaison = x.Prefix.CompareTo(y.Prefix);
            if (prefixComparaison != 0)
                return prefixComparaison;

            var majorComparaison = x.Major.CompareTo(y.Major);
            if (majorComparaison != 0)
                return majorComparaison;

            var minorComparaison = x.Minor.CompareTo(y.Minor);
            if (minorComparaison != 0)
                return minorComparaison;

            var patchComparaison = x.Patch.CompareTo(y.Patch);
            if (patchComparaison != 0)
                return patchComparaison;

            var fixComparaison = x.Fix.CompareTo(y.Fix);
            if (fixComparaison != 0)
                return fixComparaison;
            var ySuffix = string.IsNullOrEmpty(y.Suffix)
                ? "zzzzzzzzz"
                : y.Suffix;

            var suffixComparaison = x.Suffix.CompareTo(ySuffix);

            return suffixComparaison != 0
                ? suffixComparaison
                : 0;
        }
    }
}
