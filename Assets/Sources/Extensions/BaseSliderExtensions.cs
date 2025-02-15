using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Extensions
{
    public static class BaseSliderExtensions
    {
        public static BaseSlider<TValueType> WithBorders<TValueType>(this BaseSlider<TValueType> slider, TValueType low,
            TValueType high)
            where TValueType : IComparable<TValueType>
        {
            if (low.CompareTo(high) > 0)
                throw new ArgumentException("Low value should be lower than high");

            slider.lowValue = low;

            slider.highValue = high;

            return slider;
        }

        public static BaseSlider<float> WithLerpValue(this BaseSlider<float> slider, float lerp)
        {
            slider.value = Mathf.Lerp(slider.lowValue, slider.highValue, lerp);

            return slider;
        }
        
        public static BaseSlider<float> OnMiddleValue(this BaseSlider<float> slider)
        {
            slider.value = Mathf.Lerp(slider.lowValue, slider.highValue, 0.5f);

            return slider;
        }
    }
}