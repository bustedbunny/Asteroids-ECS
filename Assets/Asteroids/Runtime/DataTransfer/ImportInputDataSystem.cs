using Asteroids.Data;
using Asteroids.ECS;

namespace Asteroids.Runtime.DataTransfer
{
    public class ImportInputDataSystem : BaseSystem
    {
        private ComponentQuery<UserInput> _inputQuery;

        protected override void OnCreate()
        {
            _inputQuery = World.QueryStore.GetQuery<UserInput>();
            RequireForUpdate<UserInput>();
        }

        protected override void OnUpdate()
        {
            _inputQuery.GetSingleton().value = ImportData;
            ImportData = default;
        }

        public Input ImportData { get; set; }
    }
}