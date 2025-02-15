using System;
using System.Collections.Generic;
using Sources.Model.Registration;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class ApproveRegistrationScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private Button _approve;

        [SerializeField] private ProfileItem[] _profiles;

        public event Action OnBackClicked;

        public event Action OnApproveClicked;

        public void Initialize(IEnumerable<Profile> profiles)
        {
            foreach (ProfileItem profileItem in _profiles) 
                profileItem.gameObject.SetActive(false);

            int index = 0;
            
            foreach (Profile profile in profiles)
            {
                _profiles[index].gameObject.SetActive(true);
                
                _profiles[index].Name.Update(profile.Name);
                
                _profiles[index].UpdateAvatar(profile.Avatar);

                index++;
            }
        }
        
        private void Start()
        {
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
            
            _approve.onClick.AddListener(delegate
            {
                OnApproveClicked?.Invoke();
            });
        }
    }
}