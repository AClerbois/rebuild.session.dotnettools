using GitReleaseNote.Generator.Configurations;

namespace GitReleaseNote.Generator.Contracts
{
    public interface IConfigurationDisplayService
    {
        void Display(CommandLineOptions commandLineOptions);
    }
}
