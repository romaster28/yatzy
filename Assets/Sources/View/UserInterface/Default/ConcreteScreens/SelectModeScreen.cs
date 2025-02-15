using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class SelectModeScreen : DefaultBaseScreen
    {
        [SerializeField] private Button[] _selectors;

        [SerializeField] private Button _back;

        public event Action OnBackClicked;

        public event Action<int> OnSelected; 

        private void Start()
        {
            for (int i = 0; i < _selectors.Length; i++)
            {
                int i1 = i;
                
                _selectors[i].onClick.AddListener(delegate
                {
                    OnSelected?.Invoke(i1);
                });
            }
            
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
        }
    }
}