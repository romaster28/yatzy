using System;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class LeaderboardServerConfig
    {
        [SerializeField] private string _publicKey;

        [Min(0)] [SerializeField] private float _killTime = 5;

        public string PublicKey => _publicKey;

        public float KillTime => _killTime;
    }
}