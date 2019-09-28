namespace GitReleaseNote.Generator.Contracts
{
    public interface IGitVersionExtractor
    {
        string Extract(string workingDirectory);
    }
}
