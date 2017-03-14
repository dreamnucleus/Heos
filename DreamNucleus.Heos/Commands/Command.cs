using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands
{
    public abstract class Command<T> : Message where T : new()
    {
        public abstract T Parse(Response response);
        public T Empty => new T();

        protected Command(string text)
            : base(text)
        {
        }
    }
}
