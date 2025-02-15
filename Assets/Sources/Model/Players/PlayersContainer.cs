using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Model.Combination;
using Sources.Model.Registration;
using Sources.Model.Roller;
using Sources.Model.Stats;

namespace Sources.Model.Players
{
    public class PlayersContainer : IPlayersContainer
    {
        private readonly List<Player> _players = new();

        private readonly Player.Factory _factory;

        private readonly RollerFactory _rollerFactory;

        private readonly ScoreFactory _scoreFactory;

        private readonly CombinatorFactory _combinatorFactory;

        public PlayersContainer(Player.Factory factory, RollerFactory rollerFactory, ScoreFactory scoreFactory,
            CombinatorFactory combinatorFactory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _rollerFactory = rollerFactory ?? throw new ArgumentNullException(nameof(rollerFactory));
            _scoreFactory = scoreFactory ?? throw new ArgumentNullException(nameof(scoreFactory));
            _combinatorFactory = combinatorFactory ?? throw new ArgumentNullException(nameof(combinatorFactory));
        }

        public void Add(Profile profile)
        {
            _players.Add(_factory.Create(profile, _rollerFactory.Create(), _scoreFactory.Create(),
                _combinatorFactory.Create()));
        }

        public IEnumerable<Player> Get()
        {
            return _players;
        }

        public Player this[int index] => _players[index];

        public int Count => _players.Count;
        public bool Any(Func<Player, bool> condition) => _players.Any(condition);
    }
}