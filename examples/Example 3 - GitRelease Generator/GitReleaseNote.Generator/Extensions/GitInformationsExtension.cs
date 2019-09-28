using GitReleaseNote.Generator.Models;
using System.Collections.Generic;

namespace GitReleaseNote.Generator.Extensions
{
    public static class GitInformationsExtension
    {
        public static IList<GitInformation> RemoveRange(this IList<GitInformation> items, string range)
        {
            if (string.IsNullOrEmpty(range))
                return items;
            var result = VersionRange.Parse(range, out var versionRange)
                ? GetWithBoundaries(items, versionRange)
                : (IList<GitInformation>)GetTag(items, range);
            return result;
        }

        private static List<GitInformation> GetWithBoundaries(IList<GitInformation> items, VersionRange range)
        {
            var result = new List<GitInformation>();
            var iterator = items.GetEnumerator();
            ExcludeOutOfScope(range, iterator);
            AddScopedVersions(range, result, iterator);
            AddIncludeScopeBoundaries(range, result, iterator);
            return result;
        }

        private static void AddIncludeScopeBoundaries(VersionRange range, List<GitInformation> result, IEnumerator<GitInformation> iterator)
        {
            if (range.IsInclusive)
            {
                do
                {
                    result.Add(iterator.Current);
                    iterator.MoveNext();
                }
                while (iterator.Current.TagsName.Contains(range.From));
            }
        }

        private static void AddScopedVersions(VersionRange range, List<GitInformation> result, IEnumerator<GitInformation> iterator)
        {
            do
            {
                result.Add(iterator.Current);
                iterator.MoveNext();
            }
            while (!iterator.Current.TagsName.Contains(range.From));
        }

        private static void ExcludeOutOfScope(VersionRange range, IEnumerator<GitInformation> iterator)
        {
            do
            {
                iterator.MoveNext();
            }
            while (!iterator.Current.TagsName.Contains(range.To));
        }

        private static List<GitInformation> GetTag(IList<GitInformation> items, string range)
        {
            var ranges = range.Split("..");
            var result = new List<GitInformation>();
            if (ranges.Length == 0)
                throw new System.ArgumentException("The range parameter doesn't follow the rule", nameof(range));
            var iterator = items.GetEnumerator();
            do
            {
                iterator.MoveNext();
            }
            while (!iterator.Current.TagsName.Contains(ranges[1]));
            do
            {
                result.Add(iterator.Current);
                iterator.MoveNext();
            }
            while (!iterator.Current.TagsName.Contains(ranges[0]));
            do
            {
                result.Add(iterator.Current);
                iterator.MoveNext();
            }
            while (iterator.Current.TagsName.Contains(ranges[0]));
            return result;
        }
    }
}
