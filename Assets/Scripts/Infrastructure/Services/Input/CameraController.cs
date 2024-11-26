using Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Infrastructure.Services.Input
{
    public class CameraController : MonoBehaviour
    {
        public float CameraSpeed;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            CameraMoveHandler();
        }

        private void CameraMoveHandler()
        {
            Vector3 moveVector = _inputService.Axis;
            moveVector.y = 0;
            moveVector.Normalize();
            transform.Translate(moveVector * (CameraSpeed * Time.deltaTime));
        }
        
    }
}