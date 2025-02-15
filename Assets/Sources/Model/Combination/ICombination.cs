using System.Collections.Generic;

namespace Sources.Model.Combination
{
    public interface ICombination
    {
        bool Registered { get; }

        void Register();

        void Clear();

        void CountPreview(IEnumerable<int> dices);
        
        int Preview { get; }
    }
}