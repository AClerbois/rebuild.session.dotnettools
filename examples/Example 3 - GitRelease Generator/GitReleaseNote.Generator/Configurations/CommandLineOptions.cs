using CommandLine;
using System;

namespace GitReleaseNote.Generator.Configurations
{
    public class CommandLineOptions
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('s', "source", Required = false, HelpText = "Defines the git directory source. Default value: current directory.")]
        public string Source { get; set; } = Environment.CurrentDirectory;

        [Option('b', "branch", Required = false, HelpText = "Defines the git branch. Default value: master")]
        public string Branch { get; set; } = "master";
        
        [Option('d', "detail", Required = false, HelpText = "Active detailled view with all commit. Is set false we collapse all JIRA ticket in one row ")]
        public bool Detailled { get; set; } = false;
        
        [Option('e', "export", Required = false, HelpText = "Export mode. Either be csv (default value) or consoleticket (to retrieve all jira ticket in the console)")]
        public string ExportMode { get; set; } = "csv";

        [Option('f', "fileOutput", Required = false, HelpText = "Defines the name of the file output. Default value: output.csv")]
        public string FileOutput { get; set; } = "output.csv";

        [Option('r', "range", Required = false, HelpText = "Range to list commit from a tag to another one, separated with two dots '..'. Example : 1.0.0..2.0.0. ")]
        public string Range { get; set; } = string.Empty;
    }
}
