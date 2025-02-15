using Sources.View.UserInterface.Default.Common;
using UnityEngine;

namespace Sources.View.UserInterface.Default.Elements
{
    public class LeaderItem : MonoBehaviour
    {
        [SerializeField] private ValueView _title;

        [SerializeField] private ValueView _value;

        public ValueView Title => _title;

        public ValueView Value => _value;
    }
}