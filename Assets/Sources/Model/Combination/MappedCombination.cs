using System.Collections.Generic;

namespace Sources.Model.Combination
{
    public abstract class MappedCombination : BaseCombination
    {
        private readonly int[] _map;

        protected MappedCombination()
        {
            _map = new int[6];
        }

        public override int GeneratePreviewFromDices(IEnumerable<int> dices)
        {
            for (int i = 0; i < _map.Length; i++) 
                _map[i] = 0;

            foreach (var dice in dices) 
                _map[dice]++;
            
            return GeneratePreviewWithMap(dices, _map);
        }

        protected abstract int GeneratePreviewWithMap(IEnumerable<int> dices, int[] map);
    }
}