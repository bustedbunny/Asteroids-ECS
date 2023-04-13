using UnityEngine;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public class GameOverView : BaseView
    {
        [SerializeField] private GameRunner _game;

        private void OnEnable()
        {
            UI.rootVisualElement.Q<Button>().clicked += RestartGame;
        }

        private void RestartGame()
        {
            _game.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}