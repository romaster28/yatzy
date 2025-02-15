using System;
using Sources.DomainEvent;
using Sources.Model;
using Sources.Model.Game;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class MenuViewModel : IHandler<AppLaunched>, IHandler<AppClosed>
    {
        private readonly IScreensFacade _screens;

        private readonly IGameContext _gameContext;

        private MenuScreen Screen => _screens.Get<MenuScreen>();
        
        public MenuViewModel(IScreensFacade screens, IGameContext gameContext)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        }

        public void Handle(AppLaunched handle)
        {
            Screen.OnLeadersClicked += _screens.Open<LeadersScreen>;

            Screen.OnPlayClicked += _gameContext.Launch;
        }

        public void Handle(AppClosed handle)
        {
            Screen.OnLeadersClicked -= _screens.Open<LeadersScreen>;
            
            Screen.OnPlayClicked -= _gameContext.Launch;
        }
    }
}