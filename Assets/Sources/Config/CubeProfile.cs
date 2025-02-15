using System;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class CubeProfile
    {
        [SerializeField] private Sprite[] _cubes;

        [SerializeField] private Sprite[] _animation;

        public Sprite this[int index] => _cubes[index];

        public Sprite GetAnim(int index) => _animation[index];

        public int Count => _cubes.Length;
    }
}