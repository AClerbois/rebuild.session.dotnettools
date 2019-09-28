using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GitReleaseNote.Generator.Services
{
    public class GitInformationConverter : IGitInformationConverter
    {
        public IList<GitInformation> Convert(string logs)
        {
            var currentTag = "untagged-version";
            var result = new List<GitInformation>();
            foreach (string logLine in logs.Split(Configuration.EndLineSplitCharacter, StringSplitOptions.RemoveEmptyEntries))
            {
                var informations = logLine.Split(Configuration.SplitCharacter).ToList();
                var newEntry = new GitInformation
                {
                    CommitHash = informations[2],
                    CommitDate = DateTime.Parse(informations[3]),
                    AuthorName = informations[4],
                    AuthorEmail = informations[5]
                };
                newEntry = ManageTag(informations, newEntry, ref currentTag);
                newEntry.SetComment(informations[1]);
                if (!newEntry.HasError)
                    result.Add(newEntry);
            }
            return result;
        }

        private GitInformation ManageTag(List<string> informations, GitInformation newEntry, ref string currentTag)
        {
            if (string.IsNullOrEmpty(informations[0]))
            {
                newEntry.FullTagName = currentTag;
                newEntry.TagsName = currentTag.Split(',').ToList();
            }
            else
            {
                newEntry.SetTagName(informations[0]);
                currentTag = newEntry.TagsNameFormatted;
            }

            return newEntry;
        }
    }
}
