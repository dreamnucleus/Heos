using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Player;
using DreamNucleus.Heos.Infrastructure.Heos;
using DreamNucleus.Heos.Infrastructure.Telnet;

namespace DreamNucleus.Heos.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => 
            {
                var telnetClient = new SimpleTelnetClient("192.168.1.43", "192.168.1.45", "192.168.1.47");
                var commandProcessor = new CommandProcessor(new HeosClient(telnetClient, CancellationToken.None));

                var getPlayersResponse = await commandProcessor.Execute(new GetPlayersCommand(), r => r.Any(), 5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));
                Console.WriteLine();

            });
            
            Console.ReadKey();
        }
    }
}