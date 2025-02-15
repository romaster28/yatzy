using System;
using UnityEngine.UIElements;

namespace Sources.Extensions
{
    public static class BaseFieldExtensions
    {
        public static BaseField<TValueType> WithValue<TValueType>(this BaseField<TValueType> field, TValueType value)
            where TValueType : IComparable<TValueType>
        {
            field.value = value;

            return field;
        }
    }
}