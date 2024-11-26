using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Object = System.Object;

namespace Base
{
    public class Ore : MonoBehaviour, IPickUpable
    {
        public bool Engaged = false;
        public Action PickedUp;

        private ObjectPool<Ore> _pool;
        private Transform _container;

        public void SetPool(ObjectPool<Ore> pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.Release(this);
        }
    }
}
