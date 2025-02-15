using System;
using Sources.Config;
using Sources.Model.Mode;
using Sources.Model.Mode.ConcreteModes;
using Sources.Model.Players;
using Sources.Model.Registration;

namespace Sources.Model.Game.GameContextDecorators
{
    public class GameDebugContextDecorator : IGameContext
    {
        private readonly IGameContext _gameContext;

        private readonly IModesFactory _modesFactory;

        private readonly IModeContext _modeContext;

        private readonly IPlayersContainer _players;

        private readonly AvatarsConfig _config;

        public GameDebugContextDecorator(IGameContext gameContext, IModesFactory modesFactory, IModeContext modeContext,
            AvatarsConfig avatars, IPlayersContainer players)
        {
            _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
            _modesFactory = modesFactory ?? throw new ArgumentNullException(nameof(modesFactory));
            _modeContext = modeContext ?? throw new ArgumentNullException(nameof(modeContext));
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _config = avatars ?? throw new ArgumentNullException(nameof(avatars));
        }

        public void Launch()
        {
            _players.Add(new Profile("Player1", _config[0]));
            
            _players.Add(new Profile("Player2", _config[3]));
            
            _modeContext.Select(_modesFactory.Create<VersusFriendMode>());

            _gameContext.Launch();
        }

        public void End(GameResult result)
        {
            _gameContext.End(result);
        }
    }
}