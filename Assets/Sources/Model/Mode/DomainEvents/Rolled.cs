using System;
using Sources.Model.Players;

namespace Sources.Model.Mode.DomainEvents
{
    public class Rolled
    {
        public Player RolledBy { get; }

        public Rolled(Player rolledBy)
        {
            RolledBy = rolledBy ?? throw new ArgumentNullException(nameof(rolledBy));
        }
    }
}