using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Group;
using DreamNucleus.Heos.Commands.Player;

namespace DreamNucleus.Heos.Operations
{
    public class PlayHeosDevicesDemo : Operation<EmptyPayload>
    {
        private readonly int volume;
        private readonly IReadOnlyList<int> deviceIdsToMute;
        private readonly IReadOnlyList<int> deviceIdsToUngroup;
        private readonly IReadOnlyList<int> deviceIdsToPlay;

        public PlayHeosDevicesDemo(int volume, IReadOnlyList<int> deviceIdsToMute, IReadOnlyList<int> deviceIdsToUngroup, IReadOnlyList<int> deviceIdsToPlay)
        {
            this.volume = volume;
            this.deviceIdsToMute = deviceIdsToMute;
            this.deviceIdsToUngroup = deviceIdsToUngroup;
            this.deviceIdsToPlay = deviceIdsToPlay;
        }

        public override async Task<EmptyPayload> Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            await Execute(commandProcessor, cancellationToken,
                async () =>
                {
                    foreach (var deviceId in deviceIdsToMute)
                    {
                        await commandProcessor.Execute(new SetMuteCommand(deviceId, true), TimeSpan.FromMilliseconds(200));
                    }
                },
                async () =>
                {
                    foreach (var deviceId in deviceIdsToUngroup)
                    {
                        // TODO: wait for event as well
                        await commandProcessor.Execute(new UngroupCommand(deviceId), TimeSpan.FromMilliseconds(4000));
                    }
                },
                async () =>
                {
                    foreach (var deviceId in deviceIdsToPlay)
                    {
                        await commandProcessor.Execute(new PlayAuxInCommand(deviceId), TimeSpan.FromMilliseconds(200));
                    }
                },
                async () =>
                {
                    foreach (var deviceId in deviceIdsToPlay)
                    {
                        await commandProcessor.Execute(new SetVolumeCommand(deviceId, volume), TimeSpan.FromMilliseconds(200));
                    }
                    foreach (var deviceId in deviceIdsToPlay)
                    {
                        await commandProcessor.Execute(new SetMuteCommand(deviceId, false), TimeSpan.FromMilliseconds(200));
                    }
                });

            return new EmptyPayload();
        }
    }
}
