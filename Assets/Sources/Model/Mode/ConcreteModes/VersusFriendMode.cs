using System;
using System.Linq;
using Sources.Config;
using Sources.DomainEvent;
using Sources.Model.Combination;
using Sources.Model.Game;
using Sources.Model.Mode.DomainEvents;
using Sources.Model.Players;

namespace Sources.Model.Mode.ConcreteModes
{
    public class VersusFriendMode : IMode
    {
        private readonly IPlayersContainer _players;

        private readonly IHandler<StateUpdated> _handler;

        private readonly RulesConfig _rules;

        private bool _first = true;
        
        private int _countCompleted;

        public VersusFriendMode(IPlayersContainer players, IHandler<StateUpdated> handler, RulesConfig rulesConfig)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _rules = rulesConfig ?? throw new ArgumentNullException(nameof(rulesConfig));
        }

        private Player Current => _players[_first ? 0 : 1];

        public event Action<GameResult> Ended;

        public void Start()
        {
            _players[0].RemainRolls = _rules.RollsPerMove;
            _players[1].RemainRolls = _rules.RollsPerMove;

            _handler.Handle(new StateUpdated(_players[0], _players));
        }

        public void Roll()
        {
            if (Current.RemainRolls <= 0)
                throw new InvalidOperationException("No rolls");

            Current.Combinator
                .CountAllPreviews(Current.Roller.Values.Where(x => x.Frozen).Select(x => x.Value));

            _handler.Handle(new StateUpdated(Current, _players));

            Current.RemainRolls--;

            Current.Roller.Roll();

            Current.Combinator.CountAllPreviews(Current.Roller.Values.Select(x => x.Value));

            _handler.Handle(new StateUpdated(Current, _players, true));
        }

        public void FreezeCube(int cubeIndex)
        {
            if (Current.RemainRolls == _rules.RollsPerMove)
                return;

            var roller = Current.Roller;

            if (roller.IsFrozen(cubeIndex))
                roller.UnFreeze(cubeIndex);
            else
                roller.Freeze(cubeIndex);

            _handler.Handle(new StateUpdated(Current, _players));
        }

        public void RegisterCombination(int combinationIndex)
        {
            if (Current.RemainRolls == _rules.RollsPerMove)
                return;

            ICombinator combinator = Current.Combinator;

            if (combinator[combinationIndex].Registered)
                throw new InvalidOperationException("Combination has already registered");

            if (combinator[combinationIndex].Preview <= 0 && combinator.CanAnyToTakeNotRegistered)
                return;

            combinator[combinationIndex].Register();

            Current.Score.Add(combinator[combinationIndex].Preview);

            Current.Roller.Reset();

            Current.Combinator.Clear();

            if (combinator.IsAllRegistered)
            {
                _countCompleted++;

                if (_countCompleted == 2)
                {
                    Ended?.Invoke(GetResult());

                    return;
                }
            }

            _first = !_first;

            Current.RemainRolls = _rules.RollsPerMove;

            _handler.Handle(new StateUpdated(Current, _players));
        }
        
        private GameResult GetResult()
        {
            if (_players[0].Score.Value > _players[1].Score.Value)
                return GameResult.Win;

            if (_players[0].Score.Value < _players[1].Score.Value)
                return GameResult.Lose;

            return GameResult.Draw;
        }
    }
}