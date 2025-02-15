using System;
using Sources.DomainEvent;
using Sources.Model;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class AppViewModel : IHandler<AppLaunched>
    {
        private readonly IScreensFacade _screens;

        public AppViewModel(IScreensFacade screens)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
        }

        public void Handle(AppLaunched handle)
        {
            _screens.Open<MenuScreen>(true);
        }
    }
}