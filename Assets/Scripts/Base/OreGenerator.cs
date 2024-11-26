using System;
using Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Base
{
    public class OreGenerator : MonoBehaviour
    {
        public float SpawnRange;
        public float SpawnRate;

        public ObjectPool<Ore> OrePool;

        private IGameFactory _gameFactory;
        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            OrePool = new ObjectPool<Ore>(
                createFunc: CreateOre,
                actionOnGet: FromPool,
                actionOnRelease: ToPool,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: 5,
                maxSize: 20);
            InvokeRepeating(nameof(GetOre), 1f, SpawnRate);
        }
        private Vector3 RandomPos()
        {
            Vector3 pos = Random.insideUnitCircle.Vector2ToHorizontal() * SpawnRange;
            return pos;
        }

        private Ore CreateOre()
        {
            GameObject oreObject = _gameFactory.CreateOre(RandomPos());
            Ore ore = oreObject.GetComponent<Ore>();
            ore.SetPool(OrePool);
            return ore;
        }

        private void FromPool(Ore ore)
        {
            ore.transform.position = RandomPos();
            ore.gameObject.SetActive(true);
        }

        private void GetOre()
        {
            OrePool.Get();
        }

        private void ToPool(Ore ore)
        {
            ore.gameObject.SetActive(false);
            ore.Engaged = false;
            ore.transform.SetParent(transform);
        }
    }
}
