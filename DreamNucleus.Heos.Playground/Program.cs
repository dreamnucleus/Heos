using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Group;
using DreamNucleus.Heos.Commands.Player;
using DreamNucleus.Heos.Commands.System;
using DreamNucleus.Heos.Events;
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
                // create a telnet client with a list of IP addresses
                var telnetClient = new SimpleTelnetClient("192.168.1.43", "192.168.1.45", "192.168.1.47");

                // create a HEOS client
                var heosClient = new HeosClient(telnetClient, CancellationToken.None);

                // subscribe to all events
                heosClient.EventObservable.Subscribe(o =>
                {
                    switch (o)
                    {
                        case PlayerStateChangedEvent e:
                            Console.WriteLine($"Player {e.PlayerId}: state = {e.State}");
                            break;
                        case PlayerNowPlayingProgressEvent e:
                            Console.WriteLine($"Player {e.PlayerId}: position = {e.Position}");
                            break;
                    }
                });

                // subscribe to the volume changed event
                heosClient.EventObservable.OfType<PlayerVolumeChangedEvent>().Subscribe(e =>
                {
                    Console.WriteLine($"Player {e.PlayerId}: volume = {e.Volume}, mute = {e.Mute}");
                });

                var commandProcessor = new CommandProcessor(heosClient);

                // request change events
                var setChangeEventsCommandResponse = await commandProcessor.Execute(new SetChangeEventsCommand(true));

                // get all players, ensure there is at least 1 player
                var getPlayersResponse = await commandProcessor.Execute(new GetPlayersCommand(), r => r.Any(),
                    5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));

                if (getPlayersResponse.Success)
                {
                    Console.WriteLine("Found players: " + string.Join(", ", getPlayersResponse.Payload.Select(p => p.Name)));
                }

                var getGroupsResponse = await commandProcessor.Execute(new GetGroupsCommand());

                if (getGroupsResponse.Success && getGroupsResponse.Payload.Any())
                {
                    var getGroupInfoResponse = await commandProcessor.Execute(new GetGroupInfoCommand(getGroupsResponse.Payload.First().Gid));

                    if (getGroupInfoResponse.Success)
                    {
                    }

                    var toggleGroupMuteResponse = await commandProcessor.Execute(new ToggleGroupMute(getGroupsResponse.Payload.First().Gid));

                    if (toggleGroupMuteResponse.Success)
                    {
                        Console.ReadKey();
                    }
                }
                

                Console.ReadKey();

            });

            Console.ReadKey();
        }
    }
}