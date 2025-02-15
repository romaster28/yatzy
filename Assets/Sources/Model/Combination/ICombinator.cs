using System.Collections.Generic;

namespace Sources.Model.Combination
{
    public interface ICombinator
    {
        ICombination this[int index] { get; }

        IEnumerable<ICombination> GetAllCombinations();

        int IndexOf(ICombination combination);
        
        bool IsAllRegistered { get; }

        int Count { get; }

        void Clear();

        void CountAllPreviews(IEnumerable<int> dices);
        
        bool CanAnyToTakeNotRegistered { get; }
    }
}