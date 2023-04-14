using Asteroids.Data;
using Asteroids.ECS;

namespace Asteroids.Runtime
{
    public class ImportInputData : BaseSystem
    {
        private ComponentQuery<UserInputSingleton> _inputQuery;

        protected override void OnCreate()
        {
            _inputQuery = World.QueryStore.GetQuery<UserInputSingleton>();
            RequireForUpdate<UserInputSingleton>();
        }

        protected override void OnUpdate()
        {
            _inputQuery.GetSingleton().value = ImportData;
            ImportData = default;
        }

        public UserInput ImportData { get; set; }
    }
}