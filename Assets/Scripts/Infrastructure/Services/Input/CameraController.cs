using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class CameraController : MonoBehaviour
    {
        public float Speed;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 moveVector = _inputService.Axis;
            moveVector.y = 0;
            moveVector.Normalize();
            transform.Translate(moveVector * (Speed * Time.deltaTime));
        }
    }
}