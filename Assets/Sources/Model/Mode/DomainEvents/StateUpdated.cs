using System;
using Sources.Model.Players;

namespace Sources.Model.Mode.DomainEvents
{
    public class StateUpdated
    {
        public Player Mover { get; }
        
        public IPlayersContainer Container { get; }
        
        public bool JustRolled { get; }

        public StateUpdated(Player mover, IPlayersContainer container, bool justRolled = false)
        {
            Mover = mover ?? throw new ArgumentNullException(nameof(mover));
            Container = container ?? throw new ArgumentNullException(nameof(container));
            JustRolled = justRolled;
        }
    }
}