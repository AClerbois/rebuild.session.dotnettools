using System.Collections.Generic;
using GitReleaseNote.Generator.Models;

namespace GitReleaseNote.Generator.Contracts
{
    public interface IGitInformationConverter
    {
        IList<GitInformation> Convert(string logs);
    }
}
