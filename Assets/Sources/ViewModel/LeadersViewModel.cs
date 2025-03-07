using System;
using System.Collections.Generic;
using Sources.DomainEvent;
using Sources.Extensions;
using Sources.Model;
using Sources.Model.Game.GameContextDecorators;
using Sources.Model.Leaders;
using Sources.Model.Players;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class LeadersViewModel : IHandler<AppLaunched>, IHandler<AppClosed>, IHandler<GameEnded>
    {
        private readonly IScreensFacade _screens;
        
        private readonly ILeaderBoards _leaderBoards;

        public LeadersViewModel(IScreensFacade screens, ILeaderBoards leaderBoards)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _leaderBoards = leaderBoards ?? throw new ArgumentNullException(nameof(leaderBoards));
        }

        private LeadersScreen Screen => _screens.Get<LeadersScreen>();

        public void Handle(GameEnded handle)
        {
            foreach (Player player in handle.Players) 
                _leaderBoards.Write(player);
        }

        public void Handle(AppLaunched handle)
        {
            Screen.OnBackClicked += Screen.Close;
            
            UpdateInfo();
        }

        public void Handle(AppClosed handle)
        {
            Screen.OnBackClicked -= Screen.Close;
        }

        private void UpdateInfo()
        {
            _leaderBoards.Read(delegate(IEnumerable<LeaderInfo> infos)
            {
                Screen.UpdateLeaders(infos);
            });
        }
    }
}