using Base;
using UnityEngine;

namespace Infrastructure.Services.Construct
{
    public interface IConstructService : IService
    {
        void Construct(Vector3 vector3);
        void ChooseBuilder(BaseManager builder);
    }
}