using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Config
{
    [Serializable]
    public class BotConfig
    {
        [SerializeField] private float _minThinkTime = .5f;

        [SerializeField] private float _maxThinkTime = 1.2f;

        public float GetRandomThinkTime => Random.Range(_minThinkTime, _maxThinkTime);
    }
}