using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Extensions
{
    public static class VisualElementExtensions
    {
        public static VisualElement With(this VisualElement element, params VisualElement[] additional)
        {
            foreach (VisualElement toAdd in additional)
                element.Add(toAdd);

            return element;
        }

        public static VisualElement SetBackgroundImage(this VisualElement element, Sprite image,
            ScaleMode mode = ScaleMode.ScaleToFit)
        {
            IStyle style = element.style;
            
            style.backgroundImage = new StyleBackground(image);

            return element;
        }
    }
}