using System;
using Sources.Model.Game;

namespace Sources.Model.Mode
{
    public interface IMode
    {
        void Start();

        void Roll();

        void FreezeCube(int cubeIndex);

        void RegisterCombination(int combinationIndex);

        event Action<GameResult> Ended;
    }
}