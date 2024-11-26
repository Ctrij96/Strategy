using System.Collections.Generic;
using System.Security.Cryptography;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Base
{
    public class WorkersManager : MonoBehaviour
    {
        public Queue<Worker> Workers = new Queue<Worker>();

        private IGameFactory _gameFactory;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public Worker NewWorker()
        {
            Worker worker = _gameFactory.CreateWorker(BaseSpawnPositions()).GetComponent<Worker>();
            worker.WorkManager = this;
            Workers.Enqueue(worker);
            return worker;
        }

        public Vector3 BaseSpawnPositions()
        {
            Vector3 offset = Random.insideUnitCircle.normalized.Vector2ToHorizontal() * 3;
            return transform.position + offset;
        }

        public void SendWorkerForOre(Ore ore)
        {
            Workers.Dequeue().GoForOre(ore);
        }

        public void SendWorkerToNewBase(Vector3 newBasePos)
        {
            Workers.Dequeue().GoForNewBase(newBasePos);
        }
    }
}