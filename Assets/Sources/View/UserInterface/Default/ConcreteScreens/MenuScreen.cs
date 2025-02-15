using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class MenuScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _play;

        [SerializeField] private Button _leaders;

        public event Action OnPlayClicked;

        public event Action OnLeadersClicked;

        private void Start()
        {
            _play.onClick.AddListener(delegate
            {
                OnPlayClicked?.Invoke();
            });
            
            _leaders.onClick.AddListener(delegate
            {
                OnLeadersClicked?.Invoke();
            });
        }
    }
}