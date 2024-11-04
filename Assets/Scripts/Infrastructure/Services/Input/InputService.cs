using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        public Vector3 Axis => new(UnityEngine.Input.GetAxis(Horizontal),0, UnityEngine.Input.GetAxis(Vertical));
    }
}