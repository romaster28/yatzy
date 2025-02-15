using System.Collections.Generic;

namespace Sources.Model.Combination
{
    public abstract class BaseCombination : ICombination
    {
        public bool Registered { get; private set; }
        
        public int Preview { get; private set; }
        
        public void Register()
        {
            Registered = true;
        }

        public void Clear()
        {
            if (Registered)
                return;

            Preview = 0;
        }

        public void CountPreview(IEnumerable<int> dices) => Preview = GeneratePreviewFromDices(dices);

        public abstract int GeneratePreviewFromDices(IEnumerable<int> dices);
    }
}