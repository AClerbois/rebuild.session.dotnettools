using GitReleaseNote.Generator.Configurations;

namespace GitReleaseNote.Generator.Contracts
{
    public interface IRunnerService
    {
        void Run(CommandLineOptions commandLineOptions);
    }
}
