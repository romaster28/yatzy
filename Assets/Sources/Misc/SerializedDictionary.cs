using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Misc
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        [SerializeField] private KeyValue<TKey, TValue>[] _items;

        public IEnumerable<TKey> Keys => _items.Select(x => x.Key);

        public IEnumerable<TValue> Values => _items.Select(x => x.Value);

        public TValue this[TKey key] => _items.First(x => x.Key.Equals(key)).Value;
        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            return _items.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}