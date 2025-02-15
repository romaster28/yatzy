using System;
using Sources.Config;
using Sources.Model.Mode.ConcreteModes;
using Sources.Model.Players;
using Sources.Model.Registration;

namespace Sources.Model.Mode.InitializeModesChainHandlers
{
    public class VersusBotInitializeHandler : InitializeModeChainHandler
    {
        private readonly AvatarsConfig _config;

        public VersusBotInitializeHandler(InitializeModeChainHandler next, IRegistrar registrar,
            IPlayersContainer players, AvatarsConfig config) : base(next, registrar, players)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override bool CheckCondition(IMode mode)
        {
            return mode is VersusBotMode;
        }

        protected override void MakeHandle(IMode mode, Action success, Action failed)
        {
            _registrar.Register(delegate(Profile[] profiles)
            {
                _players.Add(profiles[0]);

                _players.Add(CreateBotProfile());
                
                success?.Invoke();
            }, failed, 1);
        }

        private Profile CreateBotProfile() => new("Bot", _config.Bot);
    }
}