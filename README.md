# Heos

```
var heosSocket = new SimpleHeosTelnetClient("192.168.1.43", "192.168.1.45", "192.168.1.47");

var commandProcessor = new CommandProcessor(new HeosClient(heosSocket, CancellationToken.None));

var getPlayersResponse = await commandProcessor.Execute(new GetPlayersCommand(), r => r.Any(), 5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));
```
