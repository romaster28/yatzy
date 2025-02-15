using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Model.Leaders;
using Sources.View.UserInterface.Default.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.ConcreteScreens
{
    public class LeadersScreen : DefaultBaseScreen
    {
        [SerializeField] private Button _back;

        [SerializeField] private LeaderItem[] _leaderItems;

        public event Action OnBackClicked;

        public void UpdateLeaders(IEnumerable<LeaderInfo> info)
        {
            LeaderInfo[] infoes = info.ToArray();
            
            for (int i = 0; i < _leaderItems.Length; i++)
            {
                _leaderItems[i].gameObject.SetActive(i < infoes.Length);
                
                if (i >= infoes.Length)
                    continue;
                
                _leaderItems[i].Title.Update(infoes[i].Name);
                
                _leaderItems[i].Value.Update(infoes[i].Score);
            }
        }

        private void Start()
        {
            _back.onClick.AddListener(delegate
            {
                OnBackClicked?.Invoke();
            });
        }
    }
}