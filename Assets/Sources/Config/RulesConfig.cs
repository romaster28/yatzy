using System;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class RulesConfig
    {
        [SerializeField] private int _rollsPerMove = 3;

        public int RollsPerMove => _rollsPerMove;
    }
}