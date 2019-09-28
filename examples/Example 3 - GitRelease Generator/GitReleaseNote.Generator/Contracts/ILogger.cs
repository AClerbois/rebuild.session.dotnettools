namespace GitReleaseNote.Generator.Contracts
{
    public interface ILogger
    {
        void Display(string message);
        void Log(string message);
    }
}
