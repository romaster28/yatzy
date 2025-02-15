using System;
using System.Linq;
using Sources.DomainEvent;
using Sources.Model.Mode;
using Sources.Model.Players;

namespace Sources.Model.Game.GameContextDecorators
{
    public class GameContextDomainEventDecorator : IGameContext
    {
        private readonly IGameContext _gameContext;

        private readonly IHandler<GameLaunched> _handler;

        private readonly IHandler<GameEnded> _endHandler;

        private readonly IModeContext _modeContext;

        private readonly IPlayersContainer _players;

        public GameContextDomainEventDecorator(IGameContext gameContext, IHandler<GameLaunched> handler,
            IHandler<GameEnded> endHandler, IModeContext modeContext, IPlayersContainer players)
        {
            _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _endHandler = endHandler ?? throw new ArgumentNullException(nameof(endHandler));
            _modeContext = modeContext;
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        public void Launch()
        {
            _gameContext.Launch();

            _handler.Handle(new GameLaunched());
            
            _modeContext.Selected.Ended += End;
        }

        public void End(GameResult result)
        {
            _gameContext.End(result);
            
            _endHandler.Handle(new GameEnded(result, _players.Get().ToArray()));
        }
    }

    public class GameLaunched
    {
    }

    public class GameEnded
    {
        public GameResult Result { get; }
        
        public Player[] Players { get; }

        public GameEnded(GameResult result, Player[] players)
        {
            Result = result;
            Players = players;
        }
    }
}