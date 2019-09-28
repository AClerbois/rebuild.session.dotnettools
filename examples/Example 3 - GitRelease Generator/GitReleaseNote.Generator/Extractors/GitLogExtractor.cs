using System.Diagnostics;
using GitReleaseNote.Generator.Configurations;
using GitReleaseNote.Generator.Contracts;

namespace GitReleaseNote.Generator.Extractors
{
    public class GitLogExtractor : IGitLogExtractor
    {
        private const string gitCommand = "git";
        private readonly ILogger logger;

        public GitLogExtractor(ILogger logger)
        {
            this.logger = logger;
        }

        public string Extract(string workingDirectory, string branch = "master")
        {
            string output;
            logger.Log("Git Extraction - Start");
            var gitArguments = $"log {branch} -a --pretty=oneline --decorate=full --pretty=\"%d{Configuration.SplitCharacter}%s{Configuration.SplitCharacter}%H{Configuration.SplitCharacter}%aI{Configuration.SplitCharacter}%an{Configuration.SplitCharacter}%ae";
            using (var process = new Process())
            {
                process.StartInfo.FileName = gitCommand;
                process.StartInfo.Arguments = gitArguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                logger.Log($"Git command used: {gitArguments}");

                process.Start();

                var reader = process.StandardOutput;
                output = reader.ReadToEnd();

                process.WaitForExit();
            }
            logger.Log("Git Extraction - End");
            return output;
        }
    }
}
