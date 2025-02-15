using System;

namespace Sources.Model.Stats
{
    public class Score : IScore
    {
        public int Value { get; private set; }
        
        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Value += amount;
        }
    }
}