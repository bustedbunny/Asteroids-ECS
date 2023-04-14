using Unity.Mathematics;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public class HudView : BaseView
    {
        private Label _coordinates;
        private Label _rotation;
        private Label _velocity;

        private void OnEnable()
        {
            _coordinates = UI.rootVisualElement.Q<Label>("label-coordinates");
            _rotation = UI.rootVisualElement.Q<Label>("label-rotation");
            _velocity = UI.rootVisualElement.Q<Label>("label-velocity");
        }

        public void UpdateValues(float2 coordinates, float rotation, float velocity)
        {
            _coordinates.text = $"Coordinates: {coordinates.x:F2}:{coordinates.y:F2}";
            _rotation.text = $"Rotation: {rotation:F2}";
            _velocity.text = $"Velocity: {velocity:F2}";
        }
    }
}