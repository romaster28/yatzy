using UnityEngine;

namespace Sources.Model.Stats
{
    public class RollsCountPlayerPrefs : IRollsCount
    {
        private const string Key = "RollsCount";
        
        public int Value
        {
            get => PlayerPrefs.GetInt(Key);
            set => PlayerPrefs.SetInt(Key, value);
        }
        public void Add()
        {
            Value++;
        }
    }
}