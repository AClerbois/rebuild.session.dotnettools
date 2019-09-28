namespace GitReleaseNote.Generator.Models
{
    public class VersionRange
    {
        public static bool Parse(string value, out VersionRange result)
        {
            if (value.Contains(".."))
            {
                var ranges = value.Split("..");
                result = new VersionRange
                {
                    IsInclusive = true,
                    From = ranges[0],
                    To = ranges[1]
                };
                return true;
            }
            result = null;
            return false;
        }

        public string From { get; internal set; }
        public string To { get; internal set; }
        public bool IsInclusive { get; internal set; }
    }

}
