using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

namespace Sources.Model.Combination
{
    public class Combinator : ICombinator
    {
        private readonly ICombination[] _combinations;

        public Combinator(ICombination[] combinations)
        {
            _combinations = combinations ?? throw new ArgumentNullException(nameof(combinations));
        }

        public ICombination this[int index] => _combinations[index];

        public IEnumerable<ICombination> GetAllCombinations() => _combinations;
        
        public int IndexOf(ICombination combination) => _combinations.IndexOf(combination);

        public bool IsAllRegistered => _combinations.All(x => x.Registered);

        public bool CanAnyToTakeNotRegistered => _combinations.Where(x => !x.Registered).Any(x => x.Preview > 0);

        public int Count => _combinations.Length;

        public void Clear()
        {
            foreach (ICombination combination in _combinations)
            {
                if (!combination.Registered)
                    combination.Clear();
            }
        }

        public void CountAllPreviews(IEnumerable<int> dices)
        {
            foreach (ICombination combination in _combinations)
            {
                if (!combination.Registered)
                    combination.CountPreview(dices);
            }
        }
    }
}