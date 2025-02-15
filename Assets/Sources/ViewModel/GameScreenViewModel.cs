using System;
using Sources.DomainEvent;
using Sources.Model;
using Sources.Model.Mode;
using Sources.Model.Stats;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine.SceneManagement;

namespace Sources.ViewModel
{
    public class GameScreenViewModel : IHandler<AppLaunched>, IHandler<AppClosed>
    {
        private readonly IScreensFacade _screens;

        private readonly IRollsCount _rolls;

        private readonly IModeContext _mode;

        public GameScreenViewModel(IScreensFacade screens, IRollsCount rolls, IModeContext modeContext)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _rolls = rolls ?? throw new ArgumentNullException(nameof(rolls));
            _mode = modeContext ?? throw new ArgumentNullException(nameof(modeContext));
        }
        
        private GameScreen Screen => _screens.Get<GameScreen>();

        private void ScreenOnBackClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Handle(AppLaunched handle)
        {
            Screen.OnBackClicked += ScreenOnBackClicked;
            
            Screen.OnRollClicked += OnRollClicked;
            
            Screen.CubesView.OnClicked += OnCubeClicked;
            
            Screen.CombinatorView.OnClicked += OnCombinatorClicked;

            Screen.RollsCount.Update(_rolls.Value);
        }

        public void Handle(AppClosed handle)
        {
            Screen.OnBackClicked -= ScreenOnBackClicked;
            
            Screen.OnRollClicked -= OnRollClicked;
            
            Screen.CubesView.OnClicked -= OnCubeClicked;
            
            Screen.CombinatorView.OnClicked -= OnCombinatorClicked;
        }

        private void OnCombinatorClicked(int combination)
        {
            _mode.Selected.RegisterCombination(combination);
        }

        private void OnCubeClicked(int cube)
        {
            _mode.Selected.FreezeCube(cube);
        }

        private void OnRollClicked()
        {
            _mode.Selected.Roll();
        }
    }
}