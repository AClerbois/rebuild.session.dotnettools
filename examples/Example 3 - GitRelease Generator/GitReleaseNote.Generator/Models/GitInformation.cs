using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GitReleaseNote.Generator.Models
{
    public class GitInformation
    {
        public GitInformation()
        {
            TagsName = new List<string>();
            FullTagName = string.Empty;
        }

        public string JiraTicket { get; set; }
        public string Comment { get; set; }
        public string FullComment { get; set; }
        public string FullTagName { get; set; }
        public List<string> TagsName { get; set; }
        public string TagsNameFormatted => string.Join(",", TagsName);
        public string CommitHash { get; set; }
        public DateTime CommitDate { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public bool HasError { get; set; }

        public void SetTagName(string value)
        {
            var tagPattern = "refs/tags/([a-zA-Z0-9\\-\\.]+)";
            FullTagName = value;

            if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, tagPattern))
            {
                var matches = Regex.Matches(value, tagPattern);
                foreach (var match in matches)
                {
                    TagsName.Add(Regex.Match(match.ToString(), tagPattern).Groups[1].Value);
                }

            }
        }

        public void SetComment(string value)
        {
            try
            {
                var jiraTicketPattern = "([a-zA-Z]+-?[0-9]+)";
                FullComment = value;
                JiraTicket = Regex.Match(value, jiraTicketPattern).Value;
                if (!string.IsNullOrEmpty(JiraTicket) && JiraTicket.Length > 0)
                {
                    var comment = value.Replace(JiraTicket, "").Trim();
                    if (comment.StartsWith('-') || comment.StartsWith(':'))
                    {
                        comment = comment.Remove(0, 1).Trim();
                    }
                    Comment = comment;
                }
                else
                    HasError = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception with the value : '{value}' - {ex}");
                throw;
            }
        }
    }
}
