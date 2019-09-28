using CommandLine;
using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GitReleaseNote.Generator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .WithParsed(option =>
                {
                    var collection = new ServiceCollection();
                    collection.AddSingleton(option);
                    DependenciesLoader.Load(collection);
                    using (var serviceProvider = collection.BuildServiceProvider())
                    {
                        var service = serviceProvider.GetService<IRunnerService>();
                        service.Run(option);
                    }
                });
        }


    }
}
