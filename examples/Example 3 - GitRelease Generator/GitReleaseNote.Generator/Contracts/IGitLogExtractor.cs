namespace GitReleaseNote.Generator.Contracts
{
    public interface IGitLogExtractor
    {
        string Extract(string workingDirectory, string branch = "master");
    }
}
