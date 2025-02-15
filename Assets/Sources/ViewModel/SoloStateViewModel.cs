using System;
using Sources.Config;
using Sources.DomainEvent;
using Sources.Extensions;
using Sources.Model.Mode.DomainEvents;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine;

namespace Sources.ViewModel
{
    public class SoloStateViewModel : IHandler<StateUpdated>
    {
        private readonly IScreensFacade _screens;

        private readonly RollConfig _config;

        public SoloStateViewModel(IScreensFacade screens, RollConfig config)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private GameScreen Screen => _screens.Get<GameScreen>();

        public void Handle(StateUpdated handle)
        {
            if (handle.Container.Count > 1)
                return;
            
            Screen.SetSolo(true);

            Screen.SetDuoActive(false);
            
            Screen.Solo.gameObject.SetActive(true);
            
            Screen.Open();

            Screen.Solo.Connect(handle.Mover);

            Screen.CombinatorView.SetAllInteractable(true);
            
            if (handle.JustRolled)
            {
                Screen.CombinatorView.SetAllInteractable(false);
                
                Screen.CubesView.AnimateCubes(handle.Mover.Roller.Values, _config.GetProfile(0),
                    delegate { Screen.SetRollInteractable(false); },
                    delegate
                    {
                        Debug.Log("Completed");
                        
                        Screen.SetRollInteractable(handle.Mover.RemainRolls > 0);
                        
                        Screen.CombinatorView.SetAllInteractable(true);

                        UpdateCombinations();
                    });
            }
            else
            {
                Screen.CubesView.SetCubes(handle.Mover.Roller.Values, _config.GetProfile(0));

                Screen.SetRollInteractable(handle.Mover.RemainRolls > 0 && !handle.Mover.Roller.AllFrozen);

                UpdateCombinations();
            }

            Screen.RemainRolls.Update(handle.Mover.RemainRolls);

            void UpdateCombinations()
            {
                for (int i = 0; i < handle.Mover.Combinator.Count; i++)
                {
                    Screen.CombinatorView[i].Count.Update(handle.Mover.Combinator[i].Preview);
                    
                    Screen.CombinatorView[i].SetInteractable(!handle.Mover.Combinator[i].Registered);
                    
                    Screen.CombinatorView.SetRegistered(i, handle.Mover.Combinator[i].Registered);
                }
            }
        }
    }
}