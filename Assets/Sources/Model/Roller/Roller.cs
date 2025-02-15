using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Config;
using Random = UnityEngine.Random;

namespace Sources.Model.Roller
{
    public class Roller : IRoller
    {
        private readonly Dice[] _dices;

        private readonly RollConfig _config;

        public Roller(RollConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _dices = new Dice[config.CubesCount];
            
            for (int i = 0; i < config.CubesCount; i++) 
                _dices[i] = new Dice();
        }

        public bool IsFrozen(int cubeIndex) => _dices[cubeIndex].Frozen;

        public IEnumerable<IReadOnlyDice> Values => _dices;
        
        public bool AllFrozen => _dices.All(x => x.Frozen);
        public void Reset()
        {
            foreach (Dice dice in _dices)
            {
                dice.Value = 0;
                
                dice.Frozen = false;
            }
        }

        public void Freeze(int cubeIndex)
        {
            if (IsFrozen(cubeIndex))
                throw new InvalidOperationException($"Cube {cubeIndex} is already frozen");

            _dices[cubeIndex].Frozen = true;
        }

        public void UnFreeze(int cubeIndex)
        {
            if (!IsFrozen(cubeIndex))
                throw new InvalidOperationException($"Cube {cubeIndex} is not frozen yet");

            _dices[cubeIndex].Frozen = false;
        }

        public void Roll()
        {
            for (int i = 0; i < _dices.Length; i++)
            {
                if (_dices[i].Frozen)
                    continue;

                _dices[i].Value = RollCube();
            }
        }

        private int RollCube() => Random.Range(0, _config.Sides);
    }
}