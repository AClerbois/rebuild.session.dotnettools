using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitReleaseNote.Generator.Exports
{
    public class CsvExport : ICsvExport
    {
        private const char Separator = ';';
        private readonly bool isDetailledMode = false;
        private const string ingenicoJiraBaseUrl = "https://jira.techno.ingenico.com/browse/";

        public CsvExport(CommandLineOptions commandLineOptions)
        {
            this.isDetailledMode = commandLineOptions.Detailled;
        }

        public void Export(IList<GitInformation> items, string file)
        {
            var stream = isDetailledMode
                ? GenerateDetailled(items)
                : GenerateLowDetail(items);

            WriteFile(file, stream);
        }

        private StringBuilder GenerateDetailled(IList<GitInformation> items)
        {
            var stream = new StringBuilder();
            stream.AppendLine($"Version{Separator}Jira Ticket{Separator}Link{Separator}Comment{Separator}Author{Separator}");
            var version = string.Empty;
            foreach (var item in items)
            {
                var isNewVersion = string.Compare(version, item.TagsNameFormatted, true) != 0;
                if (isNewVersion)
                {
                    version = item.TagsNameFormatted;
                    stream.AppendLine($"{version}{Separator}{FormatterDetailled(item)}");
                }
                else
                    stream.AppendLine($"{Separator}{FormatterDetailled(item)}");
            }
            return stream;
        }

        private StringBuilder GenerateLowDetail(IList<GitInformation> items)
        {
            var stream = new StringBuilder();
            var jiraTickets = new List<string>();
            stream.AppendLine($"Version{Separator}Jira Ticket{Separator}Link{Separator}Author{Separator}");
            var version = string.Empty;
            foreach (var item in items)
            {
                var isNewVersion = string.Compare(version, item.TagsNameFormatted, true) != 0;
                if (isNewVersion)
                {
                    jiraTickets = new List<string>();
                    version = item.TagsNameFormatted;
                    jiraTickets.Add(item.JiraTicket);
                    stream.AppendLine($"{version}{Separator}{FormatterLowDetailled(item)}");
                }
                else
                {
                    if (!jiraTickets.Contains(item.JiraTicket))
                    {
                        jiraTickets.Add(item.JiraTicket);
                        stream.AppendLine($"{Separator}{FormatterLowDetailled(item)}");
                    }
                }
            }
            return stream;
        }

        private void WriteFile(string file, StringBuilder stream)
        {
            using (var fileStream = new StreamWriter(file))
            {
                fileStream.Write(stream.ToString());
                fileStream.Flush();
                fileStream.Close();
            }
        }

        private string FormatterDetailled(GitInformation item)
        {
            return $"{item.JiraTicket}{Separator}{ingenicoJiraBaseUrl}{item.JiraTicket}{Separator}{CommentFormatter(item)}{Separator}{item.AuthorName}";
        }

        private string FormatterLowDetailled(GitInformation item)
        {
            return $"{item.JiraTicket}{Separator}{ingenicoJiraBaseUrl}{item.JiraTicket}{Separator}{item.AuthorName}";
        }

        private string CommentFormatter(GitInformation item)
        {
            var result = item.Comment;
            result = result.Replace(Separator, ',');
            if (result.StartsWith('-'))
            {
                result = $"'{result}";
            }
            return result;
        }
    }
}
