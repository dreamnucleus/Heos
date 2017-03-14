using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Infrastructure
{
    public static class Sequences
    {
        private static int counter = 0;
        public static int Next()
        {
            return Interlocked.Increment(ref counter);
        }
    }
}
