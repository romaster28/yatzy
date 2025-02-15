using System;
using System.Collections.Generic;
using Sources.Model.Registration;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default.ConcreteScreens;

namespace Sources.ViewModel
{
    public class RegisterApprovalViewModel : IRegistrarApproval
    {
        private readonly IScreensFacade _screens;

        private ApproveRegistrationScreen Screen => _screens.Get<ApproveRegistrationScreen>();
        
        public RegisterApprovalViewModel(IScreensFacade screens)
        {
            _screens = screens ?? throw new ArgumentNullException(nameof(screens));
        }

        public void Approve(IEnumerable<Profile> profiles, Action approved, Action canceled)
        {
            Screen.Open();
            
            Screen.Initialize(profiles);

            Subscribe();
            
            void Subscribe()
            {
                Screen.OnBackClicked += OnBackClicked;

                Screen.OnApproveClicked += OnApproveClicked;
            }

            void UnSubscribe()
            {
                Screen.OnBackClicked -= OnBackClicked;
                
                Screen.OnApproveClicked -= OnApproveClicked;
            }

            void OnApproveClicked()
            {
                Screen.Close();
                
                approved?.Invoke();
                
                UnSubscribe();
            }

            void OnBackClicked()
            {
                Screen.Close();
                
                canceled?.Invoke();
                
                UnSubscribe();
            }
        }
    }
}