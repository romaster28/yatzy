using System;
using Sources.DomainEvent;
using Sources.Model.Game.GameContextDecorators;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class GameContextViewModel : IHandler<GameLaunched>
    {
        private readonly IScreensFacade _screens;

        public GameContextViewModel(IScreensFacade screens)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
        }

        public void Handle(GameLaunched handle)
        {
            _screens.Open<GameScreen>(true);
        }
    }
}