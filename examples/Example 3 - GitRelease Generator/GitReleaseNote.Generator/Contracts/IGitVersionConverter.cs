using System.Collections.Generic;

namespace GitReleaseNote.Generator.Contracts
{
    public interface IGitVersionConverter
    {
        IList<string> Convert(string logs);
    }
}
