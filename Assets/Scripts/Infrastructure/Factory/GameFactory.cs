using Base;
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

        public GameObject CreateConstructHandler() => 
            _assets.Instantiate(AssetPath.ConstructHandler);

        public GameObject CreateMarker(Vector3 at) => 
            _assets.Instantiate(AssetPath.MarkerPath, at);

        public GameObject CreateCameraHandler() => 
            _assets.Instantiate(AssetPath.CameraHandler);

        public GameObject CreateOre(Vector3 at) => 
            _assets.Instantiate(AssetPath.OrePath, at);

        public GameObject CreateOreGenerator() => 
            _assets.Instantiate(AssetPath.OreGenerator);
    }
}