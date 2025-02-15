using System;
using Sources.Model.Combination;
using Sources.Model.Registration;
using Sources.Model.Roller;
using Sources.Model.Stats;
using Zenject;

namespace Sources.Model.Players
{
    public class Player
    {
        public Profile Profile { get; }
        
        public IRoller Roller { get; }
        
        public IScore Score { get; }
        
        public ICombinator Combinator { get; }

        public int RemainRolls { get; set; }

        public Player(Profile profile, IRoller roller, IScore score, ICombinator combinator)
        {
            Profile = profile;
            Roller = roller ?? throw new ArgumentNullException(nameof(roller));
            Score = score ?? throw new ArgumentNullException(nameof(score));
            Combinator = combinator ?? throw new ArgumentNullException(nameof(combinator));
        }

        public class Factory : PlaceholderFactory<Profile, IRoller, IScore, ICombinator, Player>
        {
            
        }
    }
}