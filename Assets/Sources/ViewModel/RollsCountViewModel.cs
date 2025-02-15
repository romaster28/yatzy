using System;
using Sources.DomainEvent;
using Sources.Model.Stats.DomainEvents;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class RollsCountViewModel : IHandler<RollsChanged>
    {
        private readonly IScreensFacade _screens;

        public RollsCountViewModel(IScreensFacade screens)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
        }

        public void Handle(RollsChanged handle)
        {
            _screens.Get<GameScreen>().RollsCount.Update(handle.Value);
        }
    }
}