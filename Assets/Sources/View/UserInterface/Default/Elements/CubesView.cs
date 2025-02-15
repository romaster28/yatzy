using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Config;
using Sources.Model.Roller;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Sources.View.UserInterface.Default.Elements
{
    public class CubesView : MonoBehaviour
    {
        [SerializeField] private DiceView[] _views;

        [SerializeField] private float _delaySprite = .1f;

        [SerializeField] private int _countPerStep = 5;

        [SerializeField] private Sprite _defaultBackground;

        [SerializeField] private Sprite _frozenBackground;

        private bool _animating;
        
        public event Action<int> OnClicked;

        public void SetCubes(IEnumerable<IReadOnlyDice> values, CubeProfile profile)
        {
            int index = 0;

            foreach (var value in values)
            {
                DiceView view = _views[index];

                index++;

                view.SetDice(profile[value.Value]);
                
                view.SetBackground(value.Frozen ? _frozenBackground : _defaultBackground);
            }
        }

        public void AnimateCubes(IEnumerable<IReadOnlyDice> values, CubeProfile profile, Action started,
            Action completed)
        {
            int index = 0;

            int indexAnimate = 0;

            int lastIndexNotFrozen = int.MinValue;

            for (int i = 0; i < values.Count(); i++)
            {
                if (!values.ElementAt(i).Frozen)
                    lastIndexNotFrozen = Mathf.Max(i, lastIndexNotFrozen);
            }

            _animating = true;
            
            foreach (IReadOnlyDice value in values)
            {
                var view = _views[index];
                
                index++;

                if (!value.Frozen)
                {
                    indexAnimate++;
                    
                    if (indexAnimate == 1)
                        started?.Invoke();
                
                    StartCoroutine(ShowingAnimation(indexAnimate * _countPerStep, profile[value.Value],
                        () => profile.GetAnim(Random.Range(0, profile.Count)), view,
                        index == lastIndexNotFrozen + 1 ? delegate
                        {
                            _animating = false;
                            
                            completed?.Invoke();
                        } : null));
                }
            }
        }
        private IEnumerator ShowingAnimation(int steps, Sprite target, Func<Sprite> getRandom, DiceView view,
            Action completed)
        {
            var delay = new WaitForSeconds(_delaySprite);

            for (int i = 0; i < steps; i++)
            {
                view.SetDice(getRandom?.Invoke());

                yield return delay;
            }

            view.SetDice(target);

            completed?.Invoke();
        }

        private void Start()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                int i1 = i;
                
                _views[i].OnClicked += delegate
                {
                    if (_animating)
                        return;
                    
                    OnClicked?.Invoke(i1);
                };
            }
        }
    }
}