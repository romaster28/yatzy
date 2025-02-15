using System;
using Sources.Model.Mode;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class ModeAskerViewModel : IModeAsker
    {
        private readonly IScreensFacade _screens;

        private readonly IModesFactory _modesFactory;

        private readonly InitializeModeChainHandler _initializeModeChain;

        private SelectModeScreen Screen => _screens.Get<SelectModeScreen>();

        public ModeAskerViewModel(IScreensFacade screens, IModesFactory modesFactory,
            InitializeModeChainHandler initializeModeChain)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _modesFactory = modesFactory ?? throw new ArgumentNullException(nameof(modesFactory));
            _initializeModeChain = initializeModeChain ?? throw new ArgumentNullException(nameof(initializeModeChain));
        }

        public void Ask(Action<IMode> selected, Action canceled)
        {
            Screen.Open();

            Subscribe();

            void Subscribe()
            {
                Screen.OnBackClicked += Cancel;

                Screen.OnSelected += OnSelected;
            }

            void UnSubscribe()
            {
                Screen.OnBackClicked -= Cancel;

                Screen.OnSelected -= OnSelected;
            }

            void Cancel()
            {
                Screen.Close();

                UnSubscribe();
            }

            void OnSelected(int index)
            {
                IMode created = _modesFactory.Create(index);

                _initializeModeChain.Handle(created, delegate
                {
                    selected?.Invoke(created);

                    UnSubscribe();
                }, delegate
                {
                    Cancel();

                    Ask(selected, canceled);
                });
            }
        }
    }
}