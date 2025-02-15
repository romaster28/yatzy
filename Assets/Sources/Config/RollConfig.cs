using System;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class RollConfig
    {
        [Min(1)] [SerializeField] private int _cubesCount = 5;

        [Min(1)] [SerializeField] private int _sides = 6;

        [SerializeField] private CubeProfile[] _profiles;

        public int CubesCount => _cubesCount;

        public int Sides => _sides;

        public CubeProfile GetProfile(int profile) => _profiles[profile];
    }
}