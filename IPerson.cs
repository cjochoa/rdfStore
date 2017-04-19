using System.Collections.Generic;
using BrightstarDB.EntityFramework;

namespace BrightstarDB.Samples.EntityFramework.Foaf
{
    [Entity]
    public interface IPerson
    {
        string Name { get; set; }

        ICollection<string> Emails { get; set; }
    }
}
