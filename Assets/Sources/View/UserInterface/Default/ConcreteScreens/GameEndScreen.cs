using System;
using Sources.Misc;
using Sources.Model.Game;
using Sources.View.UserInterface.Default.Common;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class GameEndScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private Button _playAgain;

        [SerializeField] private PlayerEndProfileView[] _profileViews;

        [SerializeField] private Sprite _winBackground;

        [SerializeField] private Sprite _loseBackground;

        [SerializeField] private SerializedDictionary<GameResult, string> _infoResults;

        [SerializeField] private ValueView _viewResult;

        public event Action OnBackClicked;

        public event Action OnPlayAgainClicked;

        public PlayerEndProfileView GetProfileView(int index) => _profileViews[index];

        public void UpdateResult(GameResult result)
        {
            _viewResult.Update(_infoResults[result]);
        }

        public void SetWinner(int index)
        {
            for (int i = 0; i < _profileViews.Length; i++)
            {
                _profileViews[i].SetBackground(index == i ? _winBackground : _loseBackground);
            }
        }

        public void SetActiveProfiles(int count)
        {
            for (int i = 0; i < _profileViews.Length; i++)
            {
                _profileViews[i].gameObject.SetActive(i < count);
            }
        }

        private void Start()
        {
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
            
            _playAgain.onClick.AddListener(delegate
            {
                OnPlayAgainClicked?.Invoke();
            });
        }
    }
}