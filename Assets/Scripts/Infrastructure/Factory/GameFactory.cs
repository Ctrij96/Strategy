using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateBase(Vector3 at) => 
            _assets.Instantiate(AssetPath.BasePath, at);

        public GameObject CreateWorker(Vector3 at) => 
            _assets.Instantiate(AssetPath.WorkerPath, at);

        public GameObject CreateCameraHandler() => 
            _assets.Instantiate(AssetPath.CameraHandler);
    }
}