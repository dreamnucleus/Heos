# Heos

https://usa.denon.com/us/product/hometheater/receivers/avrx6300h?docname=HEOS_CLI_PROTOCOL_Specification_290616.pdf


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

| Command | Implemented |
| --- | --- |
| Register for Change Events | Yes |
| HEOS Account Check | No |
| HEOS Account Sign In | Yes |
| HEOS Account Sign Out | No |
| HEOS System Heart Beat | No |
| HEOS Speaker Reboot | No |
| Prettify JSON response | No |

## Player Commands

| Command | Implemented |
| --- | --- |
| Get Players | Yes |
| Get Player Info | Yes |
| Get Play State | Yes |
| Set Play State | Yes |
| Get Now Playing Media | Yes |
| Get Volume | Yes |
| Set Volume | Yes |
| Volume Up | Yes |
| Volume Down | Yes |
| Get Mute | Yes |
| Set Mute | Yes |
| Toggle Mute | Yes |
| Get Play Mode | Yes |
| Set Play Mode | Yes |
| Get Queue | No |
| Play Queue Item | No |
| Remove Item(s) from Queue | No |
| Save Queue as Playlist | No |
| Clear Queue | No |
| Play Next | Yes |
| Play Previous | Yes |




