using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Model.Combination
{
    public abstract class StraightCombination : MappedCombination
    {
        public abstract int StraightLength { get; }
        
        protected override int GeneratePreviewWithMap(IEnumerable<int> dices, int[] map)
        {
            int maxStraight = int.MinValue;

            int straight = 0;

            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 0)
                    straight = 0;
                else
                {
                    straight++;

                    maxStraight = Mathf.Max(maxStraight, straight);
                }
            }
            
            return maxStraight >= StraightLength ? dices.Select(x => x + 1).Sum() : 0;
        }
    }
}