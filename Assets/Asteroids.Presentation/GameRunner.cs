using System;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GraphicsDrawer _drawer;

        private GameProxy _proxy;


        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();


            _proxy = new GameProxy();
            _proxy.Update();
            _proxy.Spawn();
        }

        private void FixedUpdate()
        {
            _proxy.Update();

            _proxy.Import = default;
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
            _proxy.Import = input;
        }
    }
}