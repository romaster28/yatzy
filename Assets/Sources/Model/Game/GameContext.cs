using System;
using Sources.Model.Mode;

namespace Sources.Model.Game
{
    public class GameContext : IGameContext
    {
        private readonly IModeContext _mode;

        public GameContext(IModeContext mode)
        {
            _mode = mode ?? throw new ArgumentNullException(nameof(mode));
        }

        public void Launch()
        {
            _mode.Selected.Start();
        }

        public void End(GameResult result)
        {
            
        }
    }
}