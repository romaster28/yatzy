using System;
using System.Linq;
using Sources.Config;
using Sources.Model.Registration;
using Sources.Model.Registration.Avatar;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;
using UnityEngine;

namespace Sources.ViewModel
{
    public class RegistrarViewModel : IRegistrar
    {
        private readonly IScreensFacade _screens;

        private readonly IAvatarAsker _avatarAsker;

        private readonly IRegistrarApproval _approval;

        private readonly AvatarsConfig _avatars;

        private int _editProfile;

        public RegistrarViewModel(IScreensFacade screens, IAvatarAsker asker, IRegistrarApproval approval,
            AvatarsConfig avatars)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            _avatarAsker = asker ?? throw new ArgumentNullException(nameof(asker));
            _approval = approval ?? throw new ArgumentNullException(nameof(approval));
            _avatars = avatars ?? throw new ArgumentNullException(nameof(avatars));
        }
        
        private RegistrationInputScreen InputScreen => _screens.Get<RegistrationInputScreen>();

        public void Register(Action<Profile[]> registered, Action canceled, int count)
        {
            Profile[] result = new Profile[count];

            for (int i = 0; i < count; i++) 
                result[i] = new Profile(string.Empty, _avatars.Default);

            InputScreen.Open();
            
            InputScreen.SetPanelsActive(count);
            
            InputScreen.ClearInputs();
            
            InputScreen.ClearInputs();
            
            InputScreen.SetDoneActive(false);
            
            Subscribe();

            void Subscribe()
            {
                InputScreen.OnBackClicked += OnBackClicked;

                InputScreen.OnDoneClicked += OnDoneClicked;

                InputScreen.OnChooseAvatarClicked += OnChooseAvatar;

                InputScreen.OnNameEdited += OnNameEdited;
            }

            void UnSubscribe()
            {
                InputScreen.OnBackClicked -= OnBackClicked;

                InputScreen.OnDoneClicked -= OnDoneClicked;

                InputScreen.OnChooseAvatarClicked -= OnChooseAvatar;

                InputScreen.OnNameEdited -= OnNameEdited;
            }

            void OnChooseAvatar(int profile)
            {
                _editProfile = profile;
                
                _avatarAsker.Ask(OnAvatarSelected, result[profile].Avatar);
            }

            void OnNameEdited(int profile, string edited)
            {
                result[profile].Name = edited;

                InputScreen.SetDoneActive(result.All(x => x.Name.Length > 0));
            }

            void OnAvatarSelected(Sprite selected)
            {
                result[_editProfile].Avatar = selected;
            }

            void OnDoneClicked()
            {
                if (!result.All(x => x.Name.Length > 0))
                    throw new InvalidOperationException("Empty name try start");

                _approval.Approve(result, delegate
                {
                    registered?.Invoke(result);
                    
                    UnSubscribe();
                    
                    InputScreen.Close();
                }, null);
            }

            void OnBackClicked()
            {
                InputScreen.Close();

                UnSubscribe();

                canceled?.Invoke();
            }
        }
    }
}