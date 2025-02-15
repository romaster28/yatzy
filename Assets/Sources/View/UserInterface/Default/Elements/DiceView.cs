using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class DiceView : MonoBehaviour
    {
        [SerializeField] private Image _background;

        [SerializeField] private Image _dice;

        [SerializeField] private Button _button;

        public event Action OnClicked;
        
        public void SetBackground(Sprite background) => _background.sprite = background;

        public void SetDice(Sprite dice) => _dice.sprite = dice;

        private void Start()
        {
            _button.onClick.AddListener(delegate
            {
                OnClicked?.Invoke();
            });
        }
    }
}