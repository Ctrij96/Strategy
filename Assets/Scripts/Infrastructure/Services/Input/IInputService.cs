using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector3 Axis { get; }
        Vector3 MousePosition { get; }
        
        bool MouseClicked { get; }
    }
}