using System;
using Sources.Model.Mode;

namespace Sources.Model.Game.GameContextDecorators
{
    public class GameContextWithModeSelectDecorator : IGameContext
    {
        private readonly IGameContext _gameContext;

        private readonly IModeAsker _modeAsker;

        private readonly IModeContext _modeContext;
        
        public GameContextWithModeSelectDecorator(IGameContext gameContext, IModeAsker asker, IModeContext modeContext)
        {
            _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
            _modeAsker = asker ?? throw new ArgumentNullException(nameof(asker));
            _modeContext = modeContext ?? throw new ArgumentNullException(nameof(modeContext));
        }

        public void Launch()
        {
            _modeAsker.Ask(delegate(IMode mode)
            {
                _modeContext.Select(mode);
                
                _gameContext.Launch();
            }, delegate
            {
                // Canceled
            });
        }

        public void End(GameResult result)
        {
            _gameContext.End(result);
        }
    }
}