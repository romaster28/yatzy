using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class AvatarChooseItem : MonoBehaviour
    {
        [SerializeField] private Image _view;

        [SerializeField] private Button _button;

        [SerializeField] private GameObject _activeMark;

        public event Action OnClicked;
        
        public void SetMarkActive(bool isActive) => _activeMark.SetActive(isActive);

        public void SetView(Sprite avatar)
        {
            _view.sprite = avatar;
        }
        
        private void Start()
        {
            _button.onClick.AddListener(delegate
            {
                OnClicked?.Invoke();
            });
        }
    }
}