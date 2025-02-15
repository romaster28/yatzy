using System.Collections.Generic;
using System.Linq;

namespace Sources.Model.Combination
{
    public abstract class CountCombination : BaseCombination
    {
        public abstract int Dice { get; }
        
        public override int GeneratePreviewFromDices(IEnumerable<int> dices)
        {
            return dices.Count(x => x == Dice) * (Dice + 1);
        }
    }
}