using System;
using Sources.Model.Mode.ConcreteModes;
using Sources.Model.Players;
using Sources.Model.Registration;

namespace Sources.Model.Mode.InitializeModesChainHandlers
{
    public class VersusFriendInitializeHandler : InitializeModeChainHandler
    {
        public VersusFriendInitializeHandler(InitializeModeChainHandler next, IRegistrar registrar,
            IPlayersContainer players) : base(next, registrar, players)
        {
        }

        public override bool CheckCondition(IMode mode)
        {
            return mode is VersusFriendMode;
        }

        protected override void MakeHandle(IMode mode, Action success, Action failed)
        {
            _registrar.Register(delegate(Profile[] profiles)
            {
                _players.Add(profiles);

                success?.Invoke();
            }, failed, 2);
        }
    }
}