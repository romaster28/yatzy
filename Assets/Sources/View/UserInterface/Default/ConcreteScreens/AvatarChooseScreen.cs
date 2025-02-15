using System;
using System.Collections.Generic;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class AvatarChooseScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private Button _choose;
        
        [SerializeField] private AvatarChooseItem[] _items;

        private int _lastSelected = -1;

        private bool _firstInitialization;
        
        public event Action OnBackClicked;

        public event Action<int> OnSelected;

        public void Initialize(IEnumerable<Sprite> avatars)
        {
            int index = 0;
            
            foreach (Sprite avatar in avatars)
            {
                _items[index].SetView(avatar);

                index++;
            }
            
            if (_firstInitialization)
                return;
            
            _choose.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
            
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });

            for (int i = 0; i < _items.Length; i++)
            {
                int i1 = i;
             
                _items[i].SetMarkActive(false);
                
                _items[i].OnClicked += delegate
                {
                    OnSelected?.Invoke(i1);
                };
            }

            _firstInitialization = true;
        }

        public void SetSelected(int index)
        {
            if (_lastSelected == index)
                return;
            
            if (_lastSelected >= 0)
                _items[_lastSelected].SetMarkActive(false);

            _items[index].SetMarkActive(true);

            _lastSelected = index;
        }
    }
}