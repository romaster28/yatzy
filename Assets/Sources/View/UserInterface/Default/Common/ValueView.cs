using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Common
{
    [Serializable]
    public class ValueView
    {
        [SerializeField] private Text _view;

        [SerializeField] private string _format;

        public void Update(float value)
        {
            Update(value.ToString());
        }
        
        public void Update(int value)
        {
            Update(value.ToString());
        }
        
        public void Update(string value)
        {
            _view.text = string.IsNullOrEmpty(_format) ? value : string.Format(_format, value);
        }
    }
}