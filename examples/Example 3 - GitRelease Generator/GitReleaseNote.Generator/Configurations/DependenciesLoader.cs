using GitReleaseNote.Generator.Contracts;
using GitReleaseNote.Generator.Exports;
using GitReleaseNote.Generator.Extractors;
using GitReleaseNote.Generator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GitReleaseNote.Generator.Configurations
{
    internal static class DependenciesLoader
    {
        public static void Load(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();
            services.AddTransient<IConfigurationDisplayService, ConfigurationDisplayService>();
            services.AddSingleton<IGitLogExtractor, GitLogExtractor>();
            services.AddSingleton<IGitVersionExtractor, GitVersionExtractor>();
            services.AddSingleton<IRunnerService, RunnerService>();
            services.AddSingleton<IGitInformationConverter, GitInformationConverter>();
            services.AddSingleton<IGitVersionConverter, GitVersionConverter>();
            services.AddSingleton<ICsvExport, CsvExport>();
            services.AddSingleton<ITicketConsoleExport, TicketConsoleExport>();
        }
    }
}
