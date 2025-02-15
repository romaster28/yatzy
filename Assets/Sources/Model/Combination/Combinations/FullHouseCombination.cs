using System.Collections.Generic;
using System.Linq;

namespace Sources.Model.Combination.Combinations
{
    public class FullHouseCombination : MappedCombination
    {
        protected override int GeneratePreviewWithMap(IEnumerable<int> dices, int[] map)
        {
            if (map.Count(x => x != 0) == 2 && map.Any(x => x == 3))
                return dices.Select(x => x + 1).Sum();

            return 0;
        }
    }
}