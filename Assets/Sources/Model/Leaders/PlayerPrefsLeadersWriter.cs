using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sources.Config;
using UnityEngine;
using Zenject;

namespace Sources.Model.Leaders
{
    public class PlayerPrefsLeadersWriter : ILeadersWriter, IInitializable
    {
        private const string Key = "Leaders";

        private const char KeysSeparator = '_';

        private const char ElementsSeparator = '=';

        private readonly LeadersConfig _config;

        private readonly List<LeaderInfo> _cache = new();
        
        private List<LeaderInfo> _converted = new();

        public PlayerPrefsLeadersWriter(LeadersConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private string Saved
        {
            get => PlayerPrefs.GetString(Key);
            set => PlayerPrefs.SetString(Key, value);
        }
        
        public void Write(LeaderInfo info)
        {
            _converted.Add(info);

            _converted = _converted.OrderByDescending(x => x.Score).ToList();

            _converted = _converted.Take(_config.PoolCount).ToList();
            
            SaveConverted();
            
            Debug.Log(Saved);
        }

        public IEnumerable<LeaderInfo> Read()
        {
            _cache.Clear();

            if (string.IsNullOrEmpty(Saved))
                return _cache;

            string[] elements = Saved.Split(ElementsSeparator);

            foreach (var rawElement in elements)
            {
                string[] rawSplit = rawElement.Split(KeysSeparator);

                _cache.Add(new LeaderInfo(rawSplit[0], int.Parse(rawSplit[1])));
            }

            return _cache;
        }

        public void Initialize()
        {
            _converted = Read().ToList();
            
            Debug.Log(Saved);
        }

        private void SaveConverted()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < _converted.Count; i++)
            {
                LeaderInfo info = _converted[i];
                
                builder.Append(info.Name);

                builder.Append(KeysSeparator);

                builder.Append(info.Score);

                if (i != _converted.Count - 1)
                    builder.Append(ElementsSeparator);
            }

            Saved = builder.ToString();
        }
    }
}