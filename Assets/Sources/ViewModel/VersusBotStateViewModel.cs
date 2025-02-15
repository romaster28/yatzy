using System;
using Sources.Config;
using Sources.DomainEvent;
using Sources.Extensions;
using Sources.Model.Mode;
using Sources.Model.Mode.ConcreteModes;
using Sources.Model.Mode.DomainEvents;
using Sources.Model.Players;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine;

namespace Sources.ViewModel
{
    public class VersusBotStateViewModel : IHandler<StateUpdated>
    {
        private readonly IScreensFacade _screens;

        private readonly IModeContext _modeContext;

        private readonly RollConfig _config;

        private Player _last;

        public VersusBotStateViewModel(IScreensFacade screens, IModeContext modeContext, RollConfig config)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _modeContext = modeContext ?? throw new ArgumentNullException(nameof(modeContext));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private GameScreen Screen => _screens.Get<GameScreen>();
        
        public void Handle(StateUpdated handle)
        {
            if (handle.Container.Count != 2 || _modeContext.Selected is not VersusBotMode versusBotMode)
                return;
            
            UpdateSubscriptions(handle);

            for (int i = 0; i < handle.Container.Count; i++)
            {
                Screen.GetDuo(i).Connect(handle.Container[i]);
                
                Screen.GetDuo(i).SetActivated(handle.Mover == handle.Container[i]);
            }

            Screen.SetSolo(false);
            
            Screen.UpdateFirstPlayerName(handle.Container[0].Profile.Name);
            
            Screen.UpdateSecondPlayerName(handle.Container[1].Profile.Name);

            bool firstPlayer = handle.Mover == handle.Container[0];
            
            Screen.SetRollInteractable(handle.Mover.RemainRolls > 0 && !handle.Mover.Roller.AllFrozen);

            Screen.GetDuoCombinatorView(0).SetAllInteractable(firstPlayer);
            
            Screen.GetDuoCombinatorView(1).SetAllInteractable(firstPlayer);
            
            Screen.RemainRolls.Update(handle.Mover.RemainRolls);

            int profile = handle.Mover == handle.Container[0] ? 0 : 1;

            if (handle.JustRolled)
            {
                Screen.CubesView.AnimateCubes(handle.Mover.Roller.Values, _config.GetProfile(profile),
                    delegate
                    {
                        Screen.SetRollInteractable(false);
                        
                        Screen.GetDuoCombinatorView(0).SetAllInteractable(false);
                    },
                    delegate
                    {
                        Screen.SetRollInteractable(handle.Mover.RemainRolls > 0 && firstPlayer);
                        
                        if (firstPlayer)
                            Screen.GetDuoCombinatorView(0).SetAllInteractable(true);

                        UpdateCombinations();
                        
                        versusBotMode.OnEndCubesAnimation();
                    });
            }
            else
            {
                Screen.CubesView.SetCubes(handle.Mover.Roller.Values, _config.GetProfile(profile));

                Screen.SetRollInteractable(handle.Mover.RemainRolls > 0 && !handle.Mover.Roller.AllFrozen && firstPlayer);

                UpdateCombinations();
            }

            void UpdateCombinations()
            {
                for (int i = 0; i < handle.Container.Count; i++)
                {
                    var player = handle.Container[i];

                    for (int j = 0; j < player.Combinator.Count; j++)
                    {
                        Screen.GetDuoCombinatorView(i)[j].Count.Update(player.Combinator[j].Preview);
                        
                        Screen.GetDuoCombinatorView(i)[j].SetInteractable(!player.Combinator[j].Registered);
                        
                        Screen.GetDuoCombinatorView(i).SetRegistered(j, player.Combinator[j].Registered);
                    }
                }
            }
        }

        private void UpdateSubscriptions(StateUpdated state)
        {
            if (_last == state.Mover)
                return;

            bool isBot = state.Mover == state.Container[1];

            if (isBot)
                Screen.GetDuoCombinatorView(0).OnClicked -= _modeContext.Selected.RegisterCombination;
            else
                Screen.GetDuoCombinatorView(0).OnClicked += _modeContext.Selected.RegisterCombination;

            _last = state.Mover;
        }
    }
}