using System.Collections.Generic;

namespace Sources.Model.Roller
{
    public interface IRoller
    {
        void Freeze(int cubeIndex);

        void UnFreeze(int cubeIndex);

        bool IsFrozen(int cubeIndex);

        void Roll();
        
        IEnumerable<IReadOnlyDice> Values { get; }
        
        bool AllFrozen { get; }

        void Reset();
    }
}