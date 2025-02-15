using System;
using System.Collections;
using System.Linq;
using Sources.Config;
using Sources.DomainEvent;
using Sources.Misc;
using Sources.Model.Combination;
using Sources.Model.Game;
using Sources.Model.Mode.DomainEvents;
using Sources.Model.Players;
using UnityEngine;

namespace Sources.Model.Mode.ConcreteModes
{
    public class VersusBotMode : IMode
    {
        private readonly BotConfig _botConfig;

        private readonly RulesConfig _rules;

        private readonly IPlayersContainer _players;

        private readonly IHandler<StateUpdated> _handler;

        private readonly AsyncProcessor _asyncProcessor;

        private bool _playerMove = true;

        private int _countCompleted;

        private Player Current => _players[_playerMove ? 0 : 1];

        public VersusBotMode(BotConfig botConfig, RulesConfig rules, IPlayersContainer players,
            IHandler<StateUpdated> handler, AsyncProcessor asyncProcessor)
        {
            _botConfig = botConfig ?? throw new ArgumentNullException(nameof(botConfig));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _asyncProcessor = asyncProcessor ? asyncProcessor : throw new ArgumentNullException(nameof(asyncProcessor));
        }

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

            _playerMove = !_playerMove;

            Current.RemainRolls = _rules.RollsPerMove;

            _handler.Handle(new StateUpdated(Current, _players));

            if (!_playerMove)
                _asyncProcessor.StartCoroutine(WaitRolling());
        }

        public void OnEndCubesAnimation()
        {
            if (_playerMove)
                return;

            Player bot = _players[1];

            ICombination targetCombination = bot.Combinator.GetAllCombinations().TakeLast(7).Take(6)
                .FirstOrDefault(x => x.Preview > 0 && !x.Registered);

            if (targetCombination == null)
            {
                if (bot.RemainRolls > 0)
                {
                    _asyncProcessor.StartCoroutine(WaitRolling());

                    return;
                }

                targetCombination = bot.Combinator.GetAllCombinations()
                    .LastOrDefault(x => !x.Registered && x.Preview > 0);

                if (targetCombination == null)
                    targetCombination = bot.Combinator.GetAllCombinations().Last(x => !x.Registered);
            }

            Debug.Log(bot.Combinator.IndexOf(targetCombination));
            
            Debug.Log(targetCombination.GetType().Name);
            
            RegisterCombination(bot.Combinator.IndexOf(targetCombination));
        }

        private GameResult GetResult()
        {
            if (_players[0].Score.Value > _players[1].Score.Value)
                return GameResult.Win;

            if (_players[0].Score.Value < _players[1].Score.Value)
                return GameResult.Lose;

            return GameResult.Draw;
        }

        private IEnumerator WaitRolling()
        {
            yield return new WaitForSeconds(_botConfig.GetRandomThinkTime);

            Roll();
        }
    }
}