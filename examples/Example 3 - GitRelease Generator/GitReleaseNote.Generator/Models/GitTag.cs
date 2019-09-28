using System.Text.RegularExpressions;

namespace GitReleaseNote.Generator.Models
{
    public class GitTag
    {
        public static GitTag Parse(string value)
        {
            var newTag = new GitTag();
            var regexTag = new Regex("([a-zA-Z\\-]+)?(\\d+)(\\.(\\d+))?(\\.(\\d+))?(\\.(\\d+))?([a-zA-Z0-9\\-]+)?");
            var values = regexTag.Match(value);
            if (values.Success)
            {
                newTag.Prefix = values.Groups[1].Value;
                newTag.Suffix = values.Groups[9].Value ;

                if (int.TryParse(values.Groups[2].Value, out int major))
                    newTag.Major = major;
                if (int.TryParse(values.Groups[4].Value, out int minor))
                    newTag.Minor = minor;
                if (int.TryParse(values.Groups[6].Value, out int patch))
                    newTag.Patch = patch;
                if (int.TryParse(values.Groups[8].Value, out int fix))
                    newTag.Fix = fix;
            }

            newTag.StringValue = value;
            return newTag;
        }

        public int Major { get; internal set; }
        public int Minor { get; internal set; }
        public int Patch { get; internal set; }
        public int Fix { get; internal set; }
        public string Suffix { get; internal set; }
        public string Prefix { get; internal set; }
        public string StringValue { get; internal set; }
    }
}
