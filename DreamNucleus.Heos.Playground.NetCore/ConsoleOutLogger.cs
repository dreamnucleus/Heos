using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;

namespace DreamNucleus.Heos.Playground.NetCore
{
    // https://github.com/net-commons/common-logging/tree/master/src/Common.Logging/Logging/Simple
    // TODO: remove, were missing in NuGet?
    public class ConsoleOutLogger : AbstractSimpleLogger
    {
        private static readonly Dictionary<LogLevel, ConsoleColor> Colors = new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Fatal, ConsoleColor.Red },
            { LogLevel.Error, ConsoleColor.Yellow },
            { LogLevel.Warn, ConsoleColor.Magenta },
            { LogLevel.Info, ConsoleColor.White },
            { LogLevel.Debug, ConsoleColor.Gray },
            { LogLevel.Trace, ConsoleColor.DarkGray },
        };

        public ConsoleOutLogger(string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
        }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            var stringBuilder = new StringBuilder();
            FormatOutput(stringBuilder, level, message, exception);

            if (Colors.TryGetValue(level, out var color))
            {
                var originalColor = Console.ForegroundColor;
                try
                {
                    Console.ForegroundColor = color;
                    Console.Out.WriteLine(stringBuilder.ToString());
                }
                finally
                {
                    Console.ForegroundColor = originalColor;
                }
            }
        }
    }
}
