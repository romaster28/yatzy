using System;
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
        
        private readonly ILeadersWriter _leadersWriter;

        public LeadersViewModel(IScreensFacade screens, ILeadersWriter leadersWriter)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _leadersWriter = leadersWriter ?? throw new ArgumentNullException(nameof(leadersWriter));
        }

        private LeadersScreen Screen => _screens.Get<LeadersScreen>();

        public void Handle(GameEnded handle)
        {
            foreach (Player player in handle.Players) 
                _leadersWriter.Write(player);
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
            Screen.UpdateLeaders(_leadersWriter.Read());
        }
    }
}