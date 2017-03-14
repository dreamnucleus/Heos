# HEOS CLI Client

A .Net [HEOS CLI](
https://usa.denon.com/us/product/hometheater/receivers/avrx6300h?docname=HEOS_CLI_PROTOCOL_Specification_290616.pdf) client using Reactive Extensions.



```cs
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

```

# CLI Coverage

## System Commands

| Command | Implemented | Object |
| --- | --- | --- |
| Register for Change Events | Yes | SetChangeEventsCommand |
| HEOS Account Check | No |  |
| HEOS Account Sign In | Yes | SignInCommand |
| HEOS Account Sign Out | No |  |
| HEOS System Heart Beat | No |  |
| HEOS Speaker Reboot | No |  |
| Prettify JSON response | No |  |

## Player Commands

| Command | Implemented | Object |
| --- | --- | --- |
| Get Players | Yes | GetPlayersCommand |
| Get Player Info | No |  |
| Get Play State | Yes | GetPlayStateCommand |
| Set Play State | Yes | SetPlayStateCommand |
| Get Now Playing Media | Yes | GetNowPlayingCommand |
| Get Volume | Yes | GetVolumeCommand |
| Set Volume | Yes | SetVolumeCommand |
| Volume Up | Yes | VolumeUpCommand |
| Volume Down | Yes | VolumeDownCommand |
| Get Mute | Yes | GetMuteCommand |
| Set Mute | Yes | SetMuteCommand |
| Toggle Mute | Yes | ToggleMuteCommand |
| Get Play Mode | No |  |
| Set Play Mode | Yes | SetPlayModeCommand |
| Get Queue | Yes | GetQueueCommand |
| Play Queue Item | No |  |
| Remove Item(s) from Queue | No |  |
| Save Queue as Playlist | No |  |
| Clear Queue | No |  |
| Play Next | Yes | PlayNextCommand |
| Play Previous | Yes | PlayPreviousCommand |




