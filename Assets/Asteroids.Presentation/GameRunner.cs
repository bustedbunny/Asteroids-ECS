using Asteroids.Presentation.UI;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GameRunner : MonoBehaviour
    {
        private GraphicsDrawer _drawer;
        private InputController _inputController;
        [SerializeField] private GameOverView _gameOverUi;
        [SerializeField] private HudView _hud;

        private GameProxy _proxy;

        private void Awake()
        {
            _drawer = GetComponent<GraphicsDrawer>();
            _inputController = GetComponent<InputController>();
            _proxy = new();
        }

        private void OnEnable()
        {
            _proxy.Spawn();
            _hud.gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            _proxy.Update();
            _proxy.import = default;

            if (_proxy.Export.gameOver)
            {
                OnGameOver();
                return;
            }

            _hud.UpdateValues(_proxy.Export.hudData);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
            _gameOverUi.gameObject.SetActive(true);
            _gameOverUi.SetScore(_proxy.Export.hudData.score);
            _hud.gameObject.SetActive(false);
        }

        private void Update()
        {
            _drawer.Draw(_proxy.Export.data);
            _inputController.UpdateInput(ref _proxy.import);
        }
    }
}