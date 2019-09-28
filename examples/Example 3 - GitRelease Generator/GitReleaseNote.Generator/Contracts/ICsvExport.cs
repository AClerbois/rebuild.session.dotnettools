using System.Collections.Generic;
using GitReleaseNote.Generator.Models;

namespace GitReleaseNote.Generator.Contracts
{
    public interface ICsvExport
    {
        void Export(IList<GitInformation> items, string file);
    }
}
