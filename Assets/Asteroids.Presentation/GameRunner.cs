using Asteroids.Presentation.UI;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GraphicsDrawer _drawer;
        [SerializeField] private GameOverView _gameOverUi;
        [SerializeField] private HudView _hud;

        private GameProxy _proxy;
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Enable();
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
            _proxy.Import = default;

            if (_proxy.Export.gameOver)
            {
                gameObject.SetActive(false);
                _gameOverUi.gameObject.SetActive(true);
                _hud.gameObject.SetActive(false);
                return;
            }

            _hud.UpdateValues(_proxy.Export.hudData);
        }

        private void Update()
        {
            _drawer.Draw(_proxy.Export.data);


            ManageInput();
        }

        private void ManageInput()
        {
            var input = _proxy.Import;
            input.movement = _inputActions.Game.Movement.ReadValue<Vector2>();

            // Since values are read only in FixedUpdate, we need to be sure press makes it to server until reset
            input.shoot |= _inputActions.Game.Shoot.WasPressedThisFrame();
            input.laserShoot |= _inputActions.Game.LaserShoot.WasPressedThisFrame();
            _proxy.Import = input;
        }
    }
}