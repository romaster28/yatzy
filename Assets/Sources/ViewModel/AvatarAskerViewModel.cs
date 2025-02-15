using System;
using Sources.Config;
using Sources.Model.Registration.Avatar;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine;

namespace Sources.ViewModel
{
    public class AvatarAskerViewModel : IAvatarAsker
    {
        private readonly AvatarsConfig _config;

        private readonly IScreensFacade _screens;

        private Sprite _selected;
        
        public AvatarAskerViewModel(AvatarsConfig config, IScreensFacade screens)
        {
            _config = config;
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
        }
        
        private AvatarChooseScreen Screen => _screens.Get<AvatarChooseScreen>();

        public void Ask(Action<Sprite> onSelected, Sprite selected)
        {
            _selected = selected ? selected : _config[_config.DefaultIndex];

            Screen.Open();
            
            Subscribe();
            
            Screen.Initialize(_config.Avatars);

            Screen.SetSelected(_config.IndexOf(_selected));

            void Subscribe()
            {
                Screen.OnBackClicked += OnBackClicked;

                Screen.OnSelected += OnSelected;
            }

            void UnSubscribe()
            {
                Screen.OnBackClicked -= OnBackClicked;
                
                Screen.OnSelected -= OnSelected;
            }

            void OnBackClicked()
            {
                Screen.Close();
                
                UnSubscribe();
                
                onSelected?.Invoke(_selected);
            }

            void OnSelected(int index)
            {
                _selected = _config[index];
                
                Screen.SetSelected(index);
            }
        }
    }
}