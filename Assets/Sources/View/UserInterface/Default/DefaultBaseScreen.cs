using UnityEngine;

namespace Sources.View.UserInterface.Default
{
    public abstract class DefaultBaseScreen : MonoBehaviour, IScreen
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}