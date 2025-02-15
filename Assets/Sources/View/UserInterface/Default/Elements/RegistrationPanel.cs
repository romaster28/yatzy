using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class RegistrationPanel : MonoBehaviour
    {
        [SerializeField] private InputField _name;

        [SerializeField] private Button _chooseAvatar;

        public event Action OnChooseAvatarClicked;

        public event Action<string> OnNameEdited;

        public void Clear()
        {
            _name.text = string.Empty;
        }
        
        private void Start()
        {
            _chooseAvatar.onClick.AddListener(delegate
            {
                OnChooseAvatarClicked?.Invoke();
            });
            
            _name.onValueChanged.AddListener(delegate(string value)
            {
                OnNameEdited?.Invoke(value);
            });
        }
    }
}