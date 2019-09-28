using GitReleaseNote.Generator.Contracts;

namespace GitReleaseNote.Generator.Configurations
{
    public class ConfigurationDisplayService : IConfigurationDisplayService
    {
        private readonly ILogger logger;

        public ConfigurationDisplayService(
            ILogger logger)
        {
            this.logger = logger;
        }

        public void Display(CommandLineOptions commandLineOptions)
        {
            logger.Log($"Ingenico Release note generator. (powered by AClerbois)");
            logger.Log($"=======================================================");
            logger.Log($"");
            logger.Log($"FileOutput: {commandLineOptions.FileOutput}");
            logger.Log($"Source: {commandLineOptions.Source}");
            logger.Log($"Branch: {commandLineOptions.Branch}");
            logger.Log($"Detailled: {commandLineOptions.Detailled}");
            logger.Log($"Range: {commandLineOptions.Range}");
            logger.Log($"");
        }
    }
}
