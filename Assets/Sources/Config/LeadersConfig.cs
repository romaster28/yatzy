using System;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class LeadersConfig
    {
        [SerializeField] private int _poolCount = 12;

        public int PoolCount => _poolCount;
    }
}