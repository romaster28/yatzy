using Sources.View.UserInterface.Default.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class PlayerEndProfileView : MonoBehaviour
    {
        [SerializeField] private Image _avatar;

        [SerializeField] private ValueView _name;

        [SerializeField] private ValueView _score;

        [SerializeField] private Image _background;

        public ValueView Name => _name;

        public ValueView Score => _score;

        public void SetBackground(Sprite background)
        {
            _background.sprite = background;
        }

        public void SetAvatar(Sprite avatar)
        {
            _avatar.sprite = avatar;
        }
    }
}