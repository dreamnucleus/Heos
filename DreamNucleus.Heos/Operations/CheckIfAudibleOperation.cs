using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Player;

namespace DreamNucleus.Heos.Operations
{
    public class CheckIfAudibleOperation : Operation<CheckIfAudibleResult>
    {
        private readonly IReadOnlyList<int> deviceIdsToCheck;
        private readonly int audibleVolume;

        public CheckIfAudibleOperation(IReadOnlyList<int> deviceIdsToCheck, int audibleVolume)
        {
            this.deviceIdsToCheck = deviceIdsToCheck;
            this.audibleVolume = audibleVolume;
        }

        public override async Task<CheckIfAudibleResult> Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            var notAudibleCount = 0;

            foreach (var deviceId in deviceIdsToCheck)
            {
                var playState = PlayState.Unknown;
                var mute = false;
                var volume = 0;

                await Execute(commandProcessor, cancellationToken,
                    async () =>
                    {
                        var result =
                            await
                                commandProcessor.Execute(new GetPlayStateCommand(deviceId), 2, TimeSpan.FromSeconds(2));
                        if (result.Success)
                        {
                            playState = result.Payload.PlayState;
                        }
                    },
                    async () =>
                    {
                        var result =
                            await commandProcessor.Execute(new GetMuteCommand(deviceId), 2, TimeSpan.FromSeconds(2));
                        if (result.Success)
                        {
                            mute = result.Payload.Mute;
                        }
                    },
                    async () =>
                    {
                        var result =
                            await commandProcessor.Execute(new GetVolumeCommand(deviceId), 2, TimeSpan.FromSeconds(2));
                        if (result.Success)
                        {
                            volume = result.Payload.Volume;
                        }
                    });

                if (playState != PlayState.Play || mute || volume <= audibleVolume)
                {
                    notAudibleCount++;
                }
            }

            return new CheckIfAudibleResult(notAudibleCount != deviceIdsToCheck.Count);
        }
    }

    public class CheckIfAudibleResult : OperationPayload
    {
        public CheckIfAudibleResult(bool audible)
        {
            Audible = audible;
        }
        public bool Audible { get; }
    }
}
