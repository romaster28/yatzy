using System;
using System.Linq;
using Sources.Config;
using Sources.DomainEvent;
using Sources.Model.Combination;
using Sources.Model.Game;
using Sources.Model.Mode.DomainEvents;
using Sources.Model.Players;
using UnityEngine;

namespace Sources.Model.Mode.ConcreteModes
{
    public class SoloMode : IMode
    {
        private readonly IPlayersContainer _players;

        private readonly IHandler<StateUpdated> _handler;

        private readonly RulesConfig _rules;
        
        public event Action<GameResult> Ended;

        public SoloMode(IPlayersContainer players, IHandler<StateUpdated> handler, RulesConfig rules)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public void Start()
        {
            _players[0].RemainRolls = _rules.RollsPerMove;

            _handler.Handle(new StateUpdated(_players[0], _players));
        }

        public void Roll()
        {
            if (_players[0].RemainRolls <= 0)
                throw new InvalidOperationException("No rolls");
            
            _players[0].Combinator
                .CountAllPreviews(_players[0].Roller.Values.Where(x => x.Frozen).Select(x => x.Value));

            _handler.Handle(new StateUpdated(_players[0], _players));

            _players[0].RemainRolls--;

            _players[0].Roller.Roll();

            _players[0].Combinator.CountAllPreviews(_players[0].Roller.Values.Select(x => x.Value));

            _handler.Handle(new StateUpdated(_players[0], _players, true));
        }

        public void FreezeCube(int cubeIndex)
        {
            if (_players[0].RemainRolls == _rules.RollsPerMove)
                return;

            var roller = _players[0].Roller;

            if (roller.IsFrozen(cubeIndex))
                roller.UnFreeze(cubeIndex);
            else
                roller.Freeze(cubeIndex);

            _handler.Handle(new StateUpdated(_players[0], _players));
        }

        public void RegisterCombination(int combinationIndex)
        {
            if (_players[0].RemainRolls == _rules.RollsPerMove)
                return;
            
            ICombinator combinator = _players[0].Combinator;
            
            Debug.Log(combinationIndex);

            if (combinator[combinationIndex].Registered)
                throw new InvalidOperationException("Combination has already registered");
            
            if (combinator[combinationIndex].Preview <= 0 && combinator.CanAnyToTakeNotRegistered)
                return;
            
            combinator[combinationIndex].Register();
            
            _players[0].Score.Add(combinator[combinationIndex].Preview);
            
            _players[0].RemainRolls = _rules.RollsPerMove;
            
            _players[0].Roller.Reset();
            
            _players[0].Combinator.Clear();
            
            _handler.Handle(new StateUpdated(_players[0], _players));

            if (combinator.IsAllRegistered)
            {
                Debug.Log("End");
                
                Ended?.Invoke(GameResult.Win);
            }
        }
    }
}