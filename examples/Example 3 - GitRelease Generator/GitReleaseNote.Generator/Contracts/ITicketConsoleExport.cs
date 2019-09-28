using System.Collections.Generic;
using GitReleaseNote.Generator.Models;

namespace GitReleaseNote.Generator.Contracts
{
    public interface ITicketConsoleExport
    {
        void Export(IList<GitInformation> items);
    }
}
