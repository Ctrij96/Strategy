using System;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Base
{
    public class BaseMarker : MonoBehaviour
    {
        private IGameFactory _gameFactory;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Worker worker))
            {
                if (worker.WorkManager == null)
                {
                    gameObject.SetActive(false);
                    _gameFactory.CreateBase(transform.position).GetComponent<BaseManager>().AddWorker();
                    Destroy(worker.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}