using GitReleaseNote.Generator.Comparers;
using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GitReleaseNote.Generator.Services
{
    public class GitVersionConverter : IGitVersionConverter
    {
        public IList<string> Convert(string logs)
        {
            return logs.Split(Configuration.EndLineSplitCharacter, StringSplitOptions.RemoveEmptyEntries)
                .Select(GitTag.Parse)
                .OrderBy(c => c, new GitTagComparer())
                .Select(c => c.StringValue)
                .ToList();
        }
    }
}
