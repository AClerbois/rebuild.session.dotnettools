using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitReleaseNote.Generator.Exports
{
    public class TicketConsoleExport : ITicketConsoleExport
    {
        private readonly bool isDetailledMode = false;
        private const string ingenicoJiraBaseUrl = "https://jira.techno.ingenico.com/browse/";

        public TicketConsoleExport(CommandLineOptions commandLineOptions)
        {
            this.isDetailledMode = commandLineOptions.Detailled;
        }

        public void Export(IList<GitInformation> items)
        {
            var list = isDetailledMode
                ? GenerateDetailled(items)
                : GenerateLowDetail(items);

            ExportResult(list);
        }

        private IList<string> GenerateDetailled(IList<GitInformation> items)
        {
            return items.Select(c => $"{ingenicoJiraBaseUrl}{c.JiraTicket}").ToList();
        }

        private IList<string> GenerateLowDetail(IList<GitInformation> items)
        {
           return items.Select(c => c.JiraTicket).ToList();
        }

        private void ExportResult(IList<string> list)
        {
           foreach(var item in list.Distinct()){
               System.Console.WriteLine(item);
           }
        }
    }
}
