using System;
using Base;
using Infrastructure.Services.Construct;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class ConstructController : MonoBehaviour
    {
        private IInputService _inputService;
        private IConstructService _constructService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _constructService = AllServices.Container.Single<IConstructService>();
        }

        private void Update()
        {
            CheckForClick();
        }

        private void CheckForClick()
        {
            if (_inputService.MouseClicked)
            {
                if (SelectedByClicking().TryGetComponent<BaseManager>(out BaseManager manager))
                {
                    _constructService.ChooseBuilder(manager);
                }

                if (SelectedByClicking().CompareTag("Surface"))
                {
                    _constructService.Construct(ClickInfo().point);
                }
            }
        }
        
        private GameObject SelectedByClicking()
        {
            return ClickInfo().collider.gameObject;
        }

        private RaycastHit ClickInfo()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            return hit;
        }
    }
}