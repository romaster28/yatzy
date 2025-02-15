using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Sources.Config
{
    [Serializable]
    public class AvatarsConfig
    {
        [SerializeField] private Sprite[] _avatars;

        [SerializeField] private int _defaultIndex;

        [SerializeField] private Sprite _bot;

        public IEnumerable<Sprite> Avatars => _avatars;

        public Sprite Default => _avatars[_defaultIndex];

        public int DefaultIndex => _defaultIndex;

        public Sprite this[int index] => _avatars[index];

        public Sprite Bot => _bot;

        public int IndexOf(Sprite avatar) => _avatars.IndexOf(avatar);
    }
}