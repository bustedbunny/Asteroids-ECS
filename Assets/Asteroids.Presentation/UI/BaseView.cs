using UnityEngine;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public abstract class BaseView : MonoBehaviour
    {
        protected UIDocument UI { get; private set; }

        private void Awake()
        {
            UI = GetComponent<UIDocument>();
        }
    }
}