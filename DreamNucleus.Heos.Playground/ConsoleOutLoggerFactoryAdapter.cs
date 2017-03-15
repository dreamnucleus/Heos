using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace DreamNucleus.Heos.Playground
{
    // https://github.com/net-commons/common-logging/tree/master/src/Common.Logging/Logging/Simple
    // TODO: remove, were missing in NuGet?
    public class ConsoleOutLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        public ConsoleOutLoggerFactoryAdapter()
            : base(null)
        { }
        public ConsoleOutLoggerFactoryAdapter(NameValueCollection properties)
            : base(properties)
        {
        }

        public ConsoleOutLoggerFactoryAdapter(LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat)
            : base(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
        }

        protected override ILog CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName,
            string dateTimeFormat)
        {
            return new ConsoleOutLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
        }
    }
}
