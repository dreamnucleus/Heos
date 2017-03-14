using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Interfaces
{
    public interface IHeosTelnetClient
    {
        Task WriteLine(string command);
        IObservable<string> Messages { get; }
    }
}
