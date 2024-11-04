using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateBase(Vector3 at);
        GameObject CreateWorker(Vector3 at);
        GameObject CreateCameraHandler();
    }
}