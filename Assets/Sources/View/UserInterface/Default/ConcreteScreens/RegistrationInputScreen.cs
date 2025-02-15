using System;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class RegistrationInputScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private RegistrationPanel[] _panels;

        [SerializeField] private Button _done;

        public event Action OnBackClicked;

        public event Action OnDoneClicked;

        public event Action<int> OnChooseAvatarClicked;

        public event Action<int, string> OnNameEdited; 

        public RegistrationPanel GetPanel(int index) => _panels[index];

        public void SetDoneActive(bool isActive) => _done.gameObject.SetActive(isActive);

        public void ClearInputs()
        {
            foreach (RegistrationPanel panel in _panels)
            {
                panel.Clear();
            }
        }
        
        public void SetPanelsActive(int count)
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].gameObject.SetActive(i < count);
            }
        }

        private void Start()
        {
            _back.onClick.AddListener(delegate { OnBackClicked?.Invoke(); });

            _done.onClick.AddListener(delegate { OnDoneClicked?.Invoke(); });

            for (int i = 0; i < _panels.Length; i++)
            {
                int i1 = i;
                
                _panels[i].OnNameEdited += delegate(string s)
                {
                    OnNameEdited?.Invoke(i1, s);
                };
                
                _panels[i].OnChooseAvatarClicked += delegate
                {
                    OnChooseAvatarClicked?.Invoke(i1);
                };
            }
        }
    }
}