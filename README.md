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


## Group Commands

| Command | Implemented | Object |
| --- | --- | --- |
| Get Groups | Yes | GetGroupsCommand |
| Get Group Info | Yes | GetGroupInfoCommand |
| Set Group | Yes | SetGroupCommand, ModifyGroupCommand, UngroupCommand |
| Get Group Volume | Yes | GetGroupVolume |
| Set Group Volume | Yes | SetGroupVolume |
| Group Volume Up | Yes | GroupVolumeUpCommand |
| Group Volume Down | Yes | GroupVolumeDownCommand |
| Get Group Mute | Yes | GetGroupMute |
| Set Group Mute | Yes | SetGroupMute |
| Toggle Group Mute | Yes | ToggleGroupMute |


## Browse Commands

| Command | Implemented | Object |
| --- | --- | --- |
| Get Music Sources | No |  |
| Get Source Info | No |  |
| Browse Source | No |  |
| Browse Source Containers | No |  |
| Get Source Search Criteria | No |  |
| Search | No |  |
| Play Station | No |  |
| Play Preset Station | No |  |
| Play Input source | No |  |
| Add Container to Queue with Options | No |  |
| Add Track to Queue with Options | Yes | AddTrackToQueueCommand |
| Get HEOS Playlists | No |  |
| Rename HEOS Playlist | No |  |
| Delete HEOS Playlist | No |  |
| Get HEOS History | No |  |
| Retrieve Album Metadata | No |  |
| Set service option | No |  |


## Change Events (Unsolicited Responses)

| Command | Implemented | Object |
| --- | --- | --- |
| Sources Changed | Yes | SourcesChangedEvent |
| Players Changed | No |  |
| Group Changed | Yes | GroupChangedEvent |
| Source Data Changed | No |  |
| Player State Changed | Yes | PlayerStateChangedEvent |
| Player Now Playing Changed | Yes | PlayerNowPlayingChangedEvent |
| Player Now Playing Progress | Yes | PlayerNowPlayingProgressEvent |
| Player Playback Error | No |  |
| Player Queue Changed | No |  |
| Player Volume Changed | Yes | PlayerVolumeChangedEvent |
| Player Mute Changed | No |  |
| Player Repeat Mode Changed | No |  |
| Player Shuffle Mode Changed | No |  |
| Group Status Changed | No |  |
| Group Volume Changed | No |  |
| Group Mute Changed | No |  |
| User Changed | No |  |


