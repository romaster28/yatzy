using Sources.View.UserInterface.Default.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class ProfileItem : MonoBehaviour
    {
        [SerializeField] private ValueView _name;

        [SerializeField] private Image _avatar;

        public ValueView Name => _name;

        public void UpdateAvatar(Sprite avatar) => _avatar.sprite = avatar;
    }
}