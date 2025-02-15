using System.Collections.Generic;
using System.Linq;

namespace Sources.Model.Combination
{
    public abstract class IdenticalCombination : MappedCombination
    {
        public abstract int IdenticalCount { get; }

        protected override int GeneratePreviewWithMap(IEnumerable<int> dices, int[] map)
        {
            for (int dice = 0; dice < map.Length; dice++)
            {
                if (map[dice] >= IdenticalCount)
                    return dices.Select(x => x + 1).Sum();
            }

            return 0;
        }
    }
}