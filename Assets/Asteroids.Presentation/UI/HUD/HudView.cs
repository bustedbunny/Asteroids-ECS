using Asteroids.Proxy;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public class HudView : BaseView
    {
        private Label _score;
        private Label _coordinates;
        private Label _rotation;
        private Label _velocity;


        private void OnEnable()
        {
            _score = UI.rootVisualElement.Q<Label>("label-score");
            _coordinates = UI.rootVisualElement.Q<Label>("label-coordinates");
            _rotation = UI.rootVisualElement.Q<Label>("label-rotation");
            _velocity = UI.rootVisualElement.Q<Label>("label-velocity");
        }

        public void UpdateValues(ExportData.HudData data)
        {
            _score.text = $"Score: {data.score}";
            _coordinates.text = $"Coordinates: {data.coordinates.x:F2}:{data.coordinates.y:F2}";
            _rotation.text = $"Rotation: {data.rotation:F2}";
            _velocity.text = $"Velocity: {data.velocity:F2}";
        }
    }
}