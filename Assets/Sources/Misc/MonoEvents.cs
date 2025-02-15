using System;
using UnityEngine;

namespace Sources.Misc
{
    public class MonoEvents : MonoBehaviour
    {
        public event Action ApplicationQuit;

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }
    }
}