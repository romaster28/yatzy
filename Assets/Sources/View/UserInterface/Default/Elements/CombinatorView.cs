using System;
using UnityEngine;

namespace Sources.View.UserInterface.Default.Elements
{
    public class CombinatorView : MonoBehaviour
    {
        [SerializeField] private CombinationView[] _combinations;

        [SerializeField] private Sprite _normalBackground;

        [SerializeField] private Sprite _registeredBackground;

        public CombinationView this[int index] => _combinations[index];

        public event Action<int> OnClicked;

        public void SetRegistered(int index, bool registered)
        {
            _combinations[index].SetBackgroundCountSprite(registered ? _registeredBackground : _normalBackground);
        }
        
        public void SetAllInteractable(bool interactable)
        {
            foreach (CombinationView combinationView in _combinations) 
                combinationView.SetInteractable(interactable);
        }
        
        private void Start()
        {
            for (int i = 0; i < _combinations.Length; i++)
            {
                int i1 = i;
                
                _combinations[i].OnClicked += delegate
                {
                    OnClicked?.Invoke(i1);
                };
            }
        }
    }
}