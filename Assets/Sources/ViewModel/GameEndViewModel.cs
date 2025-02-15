using System;
using Sources.DomainEvent;
using Sources.Extensions;
using Sources.Model;
using Sources.Model.Game;
using Sources.Model.Game.GameContextDecorators;
using Sources.Model.Players;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine.SceneManagement;

namespace Sources.ViewModel
{
    public class GameEndViewModel : IHandler<GameEnded>, IHandler<AppLaunched>, IHandler<AppClosed>
    {
        private readonly IScreensFacade _screens;

        private readonly IPlayersContainer _players;
        
        public GameEndViewModel(IScreensFacade screens, IPlayersContainer players)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _players = players ?? throw new ArgumentNullException(nameof(players));
        }

        private GameEndScreen Screen => _screens.Get<GameEndScreen>();
        
        public void Handle(GameEnded handle)
        {
            Screen.Open();
            
            Screen.SetActiveProfiles(_players.Count);

            for (int i = 0; i < _players.Count; i++)
            {
                Screen.GetProfileView(i).ConnectWithPlayer(_players[i]);
            }
            
            if (handle.Result != GameResult.Draw)
                Screen.SetWinner(handle.Result == GameResult.Win ? 0 : 1);
            
            Screen.UpdateResult(handle.Result);
        }

        private void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Handle(AppLaunched handle)
        {
            Screen.OnPlayAgainClicked += Reload;

            Screen.OnBackClicked += Reload;
        }

        public void Handle(AppClosed handle)
        {
            Screen.OnPlayAgainClicked -= Reload;
            
            Screen.OnBackClicked -= Reload;
        }
    }
}