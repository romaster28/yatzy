using System;
using UnityEngine;

namespace Sources.Misc
{
    [Serializable]
    public class KeyValue<TKey, TValue>
    {
        [SerializeField] private TKey _key;

        [SerializeField] private TValue _value;

        public TKey Key => _key;

        public TValue Value => _value;
    }
}