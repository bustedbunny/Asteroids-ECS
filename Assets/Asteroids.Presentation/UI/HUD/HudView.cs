using Asteroids.Proxy;
using Unity.Mathematics;
using UnityEngine.UIElements;

namespace Asteroids.Presentation.UI
{
    public class HudView : BaseView
    {
        private Label _score;
        private Label _coordinates;
        private Label _rotation;
        private Label _velocity;

        private Label _laser;
        private ProgressBar _laserProgress;


        private void OnEnable()
        {
            _score = UI.rootVisualElement.Q<Label>("label-score");
            _coordinates = UI.rootVisualElement.Q<Label>("label-coordinates");
            _rotation = UI.rootVisualElement.Q<Label>("label-rotation");
            _velocity = UI.rootVisualElement.Q<Label>("label-velocity");

            _laser = UI.rootVisualElement.Q<Label>("label-laser");
            _laserProgress = UI.rootVisualElement.Q<ProgressBar>("progress-laser");
        }

        public void UpdateValues(ExportData.HudData data)
        {
            _score.text = $"Score: {data.score}";
            _coordinates.text = $"Coordinates: {data.coordinates.x:F2}:{data.coordinates.y:F2}";
            _rotation.text = $"Rotation: {data.rotation:F2}";
            _velocity.text = $"Velocity: {data.velocity:F2}";

            _laser.text = $"Laser charges: {(int)data.laserChargeData}";
            _laserProgress.value = math.modf(data.laserChargeData, out _);
        }
    }
}