using System;
using System.Linq;
using UnityEngine;

namespace Sources.View.UserInterface.ToolKit
{
    [Serializable]
    public class ToolKitScreensFacade : IScreensFacade
    {
        [SerializeField] private BaseUiToolKitScreen[] _screens;

        public void Open<T>(bool closeOthers) where T : IScreen
        {
            if (closeOthers)
                CloseAll();
            
            Open<T>();
        }

        public void Open<T>() where T : IScreen => Get<T>().Open();

        public void Close<T>() where T : IScreen => Get<T>().Close();

        public void OpenAll()
        {
            foreach (BaseUiToolKitScreen screen in _screens) 
                screen.Open();
        }

        public void CloseAll()
        {
            foreach (BaseUiToolKitScreen screen in _screens) 
                screen.Close();
        }

        public T Get<T>() where T : IScreen => (T)(IScreen)_screens.First(x => x is T);
    }
}