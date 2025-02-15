using Sources.View.UserInterface.Default.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.View.UserInterface.Default.Elements
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ValueView _name;

        [SerializeField] private ValueView _score;

        [SerializeField] private Image _avatar;

        [SerializeField] private Image _background;
        
        [SerializeField] private Color _activeBackground = Color.white;
        
        [SerializeField] private Color _inActiveBackground = Color.gray;

        public ValueView Name => _name;

        public ValueView Score => _score;

        public void SetActivated(bool activated)
        {
            _background.color = activated ? _activeBackground : _inActiveBackground;
        }
        
        public void UpdateAvatar(Sprite avatar)
        {
            _avatar.sprite = avatar;
        }
    }
}