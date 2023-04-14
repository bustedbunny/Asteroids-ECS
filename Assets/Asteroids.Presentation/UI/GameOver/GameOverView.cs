using UnityEngine;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public class GameOverView : BaseView
    {
        [SerializeField] private GameRunner _game;
        private Label _label;

        private void OnEnable()
        {
            UI.rootVisualElement.Q<Button>().clicked += RestartGame;
            _label = UI.rootVisualElement.Q<Label>("label-main");
        }

        public void SetScore(int score)
        {
            _label.text = $"Total score: {score}";
        }

        private void RestartGame()
        {
            _game.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}