using System;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class InputController : MonoBehaviour
    {
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new();
            _inputActions.Enable();
        }

        public void UpdateInput(ref ImportData input)
        {
            input.movement = _inputActions.Game.Movement.ReadValue<Vector2>();

            // Since values are read only in FixedUpdate, we need to be sure press makes it to server until reset
            input.shoot |= _inputActions.Game.Shoot.WasPressedThisFrame();
            input.laserShoot |= _inputActions.Game.LaserShoot.WasPressedThisFrame();
        }
    }
}