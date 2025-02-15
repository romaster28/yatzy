using System;
using Sources.View.UserInterface.Default.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class CombinationView : MonoBehaviour
    {
        [SerializeField] private ValueView _count;

        [SerializeField] private Button _button;

        [SerializeField] private Image _backgroundCount;
        
        public ValueView Count => _count;

        public event Action OnClicked;

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        public void SetBackgroundCountSprite(Sprite backgroundCount)
        {
            _backgroundCount.sprite = backgroundCount;
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