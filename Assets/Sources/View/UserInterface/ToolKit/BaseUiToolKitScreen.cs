using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.View.UserInterface.ToolKit
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseUiToolKitScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private StyleSheet[] _styles;
        
        private UIDocument _document;

        public void Open()
        {
            if (_document == null)
                Awake();
            
            _document.gameObject.SetActive(true);
            
            _document.rootVisualElement.Clear();

            foreach (StyleSheet style in _styles) 
                _document.rootVisualElement.styleSheets.Add(style);

            Create(_document.rootVisualElement);
        }

        public void Close()
        {
            _document.rootVisualElement.Clear();
            
            _document.gameObject.SetActive(false);
        }

        public abstract void Create(VisualElement root);

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnValidate()
        {
            DebugShow();
        }

        private void OnDrawGizmos()
        {
            DebugShow();
        }

        private void DebugShow()
        {
            if (Application.isPlaying)
                return;
            
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            if (root == null)
                return;
            
            root.Clear();
            
            foreach (StyleSheet style in _styles) 
                root.styleSheets.Add(style);
            
            Create(root);
        }

        protected T Create<T>(params string[] classNames) where T : VisualElement, new()
        {
            var element = new T();

            foreach (var className in classNames)
                element.AddToClassList(className);

            return element;
        }
    }
}