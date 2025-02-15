using System.Collections.Generic;
using System.Linq;

namespace Sources.Model.Combination.Combinations
{
    public class YatzyCombination : MappedCombination
    {
        private const int Reward = 50;
        
        protected override int GeneratePreviewWithMap(IEnumerable<int> dices, int[] map)
        {
            if (map.Any(x => x == map.Length - 1))
                return Reward;

            return 0;
        }
    }
}