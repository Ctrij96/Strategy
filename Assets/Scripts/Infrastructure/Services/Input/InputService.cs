using System;
using Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector3 Axis => new(UnityEngine.Input.GetAxis(Horizontal), 0, UnityEngine.Input.GetAxis(Vertical));
        public Vector3 MousePosition => UnityEngine.Input.mousePosition;
        public bool MouseClicked => UnityEngine.Input.GetMouseButtonDown(0);
    }
}
