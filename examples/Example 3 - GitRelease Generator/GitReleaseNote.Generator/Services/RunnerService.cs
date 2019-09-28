using System.Collections.Generic;
using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Extensions;

namespace GitReleaseNote.Generator.Services
{
    public class RunnerService : IRunnerService
    {
        private readonly IConfigurationDisplayService configurationDisplayService;
        private readonly IGitInformationConverter gitInformationConverter;
        private readonly IGitVersionConverter gitVersionConverter;
        private readonly IGitVersionExtractor gitVersionExtractor;
        private readonly IGitLogExtractor gitLogExtractor;       
        private readonly ICsvExport csvExport;
        private readonly ITicketConsoleExport ticketConsoleExport;
        private readonly ILogger logger;

        public RunnerService(
            IConfigurationDisplayService configurationDisplayService,
            IGitInformationConverter gitInformationConverter,
            IGitVersionConverter gitVersionConverter,
            IGitVersionExtractor gitVersionExtractor,
            IGitLogExtractor gitLogExtractor,
            ICsvExport csvExport,
            ITicketConsoleExport ticketConsoleExport,
            ILogger logger)
        {
            this.configurationDisplayService = configurationDisplayService;
            this.gitInformationConverter = gitInformationConverter;
            this.gitVersionConverter = gitVersionConverter;
            this.gitVersionExtractor = gitVersionExtractor;
            this.gitLogExtractor = gitLogExtractor;
            this.csvExport = csvExport;
            this.ticketConsoleExport = ticketConsoleExport;
            this.logger = logger;
        }

        public void Run(CommandLineOptions commandLineOptions)
        {
            configurationDisplayService.Display(commandLineOptions);
            var versionsRaw = gitVersionExtractor.Extract(commandLineOptions.Source);
            var versions = gitVersionConverter.Convert(versionsRaw);
            if (!IsValidVersion(versions, commandLineOptions.Range))
            {
                logger.Display($"Error - The argument 'range' doesn't match with a valid number(s) of version or the order isn't respected. - parameter value : {commandLineOptions.Range}");
                return;
            }

            var logs = gitLogExtractor.Extract(commandLineOptions.Source, commandLineOptions.Branch);
            var items = gitInformationConverter.Convert(logs).RemoveRange(commandLineOptions.Range);
            Export(commandLineOptions, items);
        }

        private void Export(CommandLineOptions commandLineOptions, IList<Models.GitInformation> items)
        {
            if (string.Compare(commandLineOptions.ExportMode, "consoleticket", true) == 0)
            {
                ticketConsoleExport.Export(items);
            }
            else
            {
                csvExport.Export(items, $"{commandLineOptions.Source}/{commandLineOptions.FileOutput}");
            }
        }

        private bool IsValidVersion(IList<string> versions, string version)
        {
            return string.IsNullOrEmpty(version)
                ? true
                : Models.VersionRange.Parse(version, out var versionRange)
                    ? versions.Contains(versionRange.From) && versions.Contains(versionRange.To) && versions.IndexOf(versionRange.From) < versions.IndexOf(versionRange.To)
                    : versions.Contains(version);
        }
    }
}
