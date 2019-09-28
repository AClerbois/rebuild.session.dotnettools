using System;
using GitReleaseNote.Generator.Contracts;

namespace GitReleaseNote.Generator.Configurations
{
    public class Logger : ILogger
    {
        private readonly CommandLineOptions commandLineOptions;

        public Logger(CommandLineOptions commandLineOptions)
        {
            this.commandLineOptions = commandLineOptions;
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message)
        {
            if (commandLineOptions.Verbose)
                Console.WriteLine(message);
        }
    }
}
