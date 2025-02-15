using System;
using UnityEngine.UIElements;

namespace Sources.Extensions
{
    public static class ButtonExtensions
    {
        public static Button AddClicked(this Button button, Action handle)
        {
            button.clicked += handle;

            return button;
        }
    }
}