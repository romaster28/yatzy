using UnityEngine.UIElements;

namespace Sources.Extensions
{
    public static class TextElementExtensions
    {
        public static T WithText<T>(this T element, string text) where T : TextElement
        {
            element.text = text;

            return element;
        }
    }
}