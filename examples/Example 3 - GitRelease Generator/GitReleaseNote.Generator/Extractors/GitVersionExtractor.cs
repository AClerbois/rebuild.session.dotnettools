using System.Diagnostics;
using GitReleaseNote.Generator.Contracts;

namespace GitReleaseNote.Generator.Extractors
{
    public class GitVersionExtractor : IGitVersionExtractor
    {
        private const string gitCommand = "git";
        private readonly ILogger logger;

        public GitVersionExtractor(ILogger logger)
        {
            this.logger = logger;
        }

        public string Extract(string workingDirectory)
        {
            logger.Log("Git Tag Extraction - Start");
            string output;
            var gitArguments = $"tag ";
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
            logger.Log("Git Tag Extraction - End");
            return output;
        }
    }
}
