using Sources.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.View.UserInterface.ToolKit.ConcreteScreens
{
    public class MenuScreen : BaseUiToolKitScreen
    {
        [SerializeField] private Sprite _back;

        [SerializeField] private Sprite _header;
        
        public override void Create(VisualElement root)
        {
            var backGround = Create<VisualElement>("background").SetBackgroundImage(_back)
                .With(Create<VisualElement>("header").SetBackgroundImage(_header));

            root.Add(backGround);
        }
    }
}