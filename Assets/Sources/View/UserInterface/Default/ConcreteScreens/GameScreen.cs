using System;
using Sources.View.UserInterface.Default.Common;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class GameScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private ValueView _rollsCount;

        [SerializeField] private PlayerView _solo;

        [SerializeField] private Button _roll;

        [SerializeField] private ValueView _remainRolls;

        [SerializeField] private CubesView _cubesView;

        [SerializeField] private CombinatorView _combinatorView;

        [SerializeField] private PlayerView[] _duo;

        [SerializeField] private Text[] _firstPlayerName;

        [SerializeField] private Text[] _secondPlayerName;

        [SerializeField] private GameObject _soloCombinations;

        [SerializeField] private GameObject _duoCombinations;

        [SerializeField] private CombinatorView[] _duoCombinatorViews;

        public event Action OnBackClicked;

        public event Action OnRollClicked;

        public ValueView RemainRolls => _remainRolls;

        public CubesView CubesView => _cubesView;

        public ValueView RollsCount => _rollsCount;

        public PlayerView Solo => _solo;

        public CombinatorView CombinatorView => _combinatorView;

        public CombinatorView GetDuoCombinatorView(int index) => _duoCombinatorViews[index];
        
        public PlayerView GetDuo(int index) => _duo[index];
        
        public void SetRollInteractable(bool interactable) => _roll.interactable = interactable;

        public void SetSolo(bool solo)
        {
            _soloCombinations.SetActive(solo);
            
            _duoCombinations.SetActive(!solo);
        }

        public void UpdateFirstPlayerName(string firstPlayerName)
        {
            foreach (Text playerName in _firstPlayerName)
            {
                playerName.text = firstPlayerName;
            }
        }

        public void UpdateSecondPlayerName(string secondPlayerName)
        {
            foreach (Text playerName in _secondPlayerName)
            {
                playerName.text = secondPlayerName;
            }
        }

        public void SetDuoActive(bool isActive)
        {
            foreach (PlayerView duoView in _duo)
            {
                duoView.gameObject.SetActive(isActive);
            }
        }

        private void Start()
        {
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
            
            _roll.onClick.AddListener(delegate
            {
                OnRollClicked?.Invoke();
            });
        }
    }
}