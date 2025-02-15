using System;
using Sources.Model.Players;
using Sources.Model.Registration;

namespace Sources.Model.Mode
{
    public abstract class InitializeModeChainHandler
    {
        protected readonly IRegistrar _registrar;

        protected readonly IPlayersContainer _players;
        
        public InitializeModeChainHandler Next { get; }

        protected InitializeModeChainHandler(InitializeModeChainHandler next, IRegistrar registrar, IPlayersContainer players)
        {
            Next = next;
            _registrar = registrar ?? throw new ArgumentNullException(nameof(registrar));
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        public void Handle(IMode mode, Action success, Action failed)
        {
            if (CheckCondition(mode))
                MakeHandle(mode, success, failed);
            else
                Next?.Handle(mode, success, failed);
        }
        
        public abstract bool CheckCondition(IMode mode);

        protected abstract void MakeHandle(IMode mode, Action success, Action failed);
    }
}