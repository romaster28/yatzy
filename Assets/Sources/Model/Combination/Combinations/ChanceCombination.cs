using System.Collections.Generic;
using System.Linq;

namespace Sources.Model.Combination.Combinations
{
    public class ChanceCombination : BaseCombination
    {
        public override int GeneratePreviewFromDices(IEnumerable<int> dices)
        {
            return dices.Sum(x => x + 1);
        }
    }
}