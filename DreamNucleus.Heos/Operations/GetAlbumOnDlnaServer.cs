using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Browse;

namespace DreamNucleus.Heos.Operations
{
    public class GetAlbumOnDlnaServer : Operation<GetDemoAlbumOnDlnaServerResult>
    {
        private readonly string dlnaServer;
        private readonly Func<BrowseSourceResponse, bool> findDlnaFunc;

        private const string LocalMusic = "Local Music";
        private static readonly Func<GetMusicSourcesResponse, bool> FindLocalMusicFunc = r => r.Name.Equals(LocalMusic, StringComparison.OrdinalIgnoreCase);

        private const string Music = "Music";
        private static readonly Func<BrowseServerResponse, bool> FindMusicFunc = r => r.Name.Equals(Music, StringComparison.OrdinalIgnoreCase);

        private const string Albums = "Albums";
        private static readonly Func<BrowseServerResponse, bool> FindAlbumsFunc = r => r.Name.Equals(Albums, StringComparison.OrdinalIgnoreCase);

        private readonly string albumName;
        private readonly Func<BrowseServerResponse, bool> findDemoAlbumFunc;

        public GetAlbumOnDlnaServer(string dlnaServer, string albumName)
        {
            this.dlnaServer = dlnaServer;
            findDlnaFunc = r => r.Name.Equals(dlnaServer, StringComparison.OrdinalIgnoreCase);
            this.albumName = albumName;
            findDemoAlbumFunc = r => r.Name.Equals(albumName, StringComparison.OrdinalIgnoreCase);
        }

        // what will this do if i cancel it, or fail
        public override async Task<GetDemoAlbumOnDlnaServerResult> Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            var localMusicSourceId = 0;
            var dlnaSourceId = 0;
            var dlnaMusicContainerId = string.Empty;
            var dlnaAlbumsContainerId = string.Empty;

            GetDemoAlbumOnDlnaServerResult result = null;

            await Execute(commandProcessor, cancellationToken,
                // Get music sources
                async () =>
                {
                    var musicSourcesResult = await commandProcessor.Execute(new GetMusicSourcesCommand(), m => m.Any(FindLocalMusicFunc), 10, TimeSpan.FromSeconds(10));
                    localMusicSourceId = musicSourcesResult.Payload.Single(FindLocalMusicFunc).Sid;
                },
                // Get DLNA server
                async () =>
                {
                    var localMusicResult = await commandProcessor.Execute(new BrowseSourceCommand(localMusicSourceId), m => m.Any(findDlnaFunc), 10, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(2));
                    dlnaSourceId = localMusicResult.Payload.Single(findDlnaFunc).Sid;
                },
                // Get music folder on DLNA server
                async () =>
                {
                    var serverMusicResult = await commandProcessor.Execute(new BrowseServerCommand(dlnaSourceId), p => p.Any(FindMusicFunc), 10, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(2));
                    dlnaMusicContainerId = serverMusicResult.Payload.Single(FindMusicFunc).Cid;
                },
                // Get albums folder on DLNA server
                async () =>
                {
                    var serverAlbumsResult = await commandProcessor.Execute(new BrowseServerContainerCommand(dlnaSourceId, dlnaMusicContainerId),
                        p => p.Any(FindAlbumsFunc), 10, TimeSpan.FromSeconds(20));
                    dlnaAlbumsContainerId = serverAlbumsResult.Payload.Single(FindAlbumsFunc).Cid;
                },
                // Get demo album container on DLNA server
                async () =>
                {
                    var serverDemoAlbumResult = await commandProcessor.Execute(new BrowseServerContainerCommand(dlnaSourceId, dlnaAlbumsContainerId),
                        p => p.Any(findDemoAlbumFunc), 10, TimeSpan.FromSeconds(20));

                    result = new GetDemoAlbumOnDlnaServerResult
                    {
                        SourceId = dlnaSourceId,
                        ContainerId = serverDemoAlbumResult.Payload.Single(findDemoAlbumFunc).Cid
                    };
                });

            return result;
        }
    }

    public class GetDemoAlbumOnDlnaServerResult : OperationPayload
    {
        public int SourceId { get; set; }
        public string ContainerId { get; set; }
    }
}
