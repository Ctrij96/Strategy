using Base;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateBase(Vector3 at);
        GameObject CreateCameraHandler();
        GameObject CreateOre(Vector3 at);
        GameObject CreateOreGenerator();
        GameObject CreateWorker(Vector3 at);
        GameObject CreateConstructHandler();
        GameObject CreateMarker(Vector3 at);
    }
}