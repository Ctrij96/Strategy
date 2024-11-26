using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Base
{
    public class Worker : MonoBehaviour
    {
        public WorkersManager WorkManager;
        public Action OreDelivered;
        
        private Ore _oreToHandle;
        private NavMeshAgent _agent;
        private Vector3 _destination;
        private bool _orePicked;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        /* private void Update()
        {
            if (_working)
            {
                _agent.destination = _destination;
            }
            else
            {
                _agent.destination = WorkManager.BaseSpawnPositions();
            }
        } */

        public void GoForOre(Ore ore)
        {
            _agent.destination = ore.transform.position;
            _oreToHandle = ore;
            _oreToHandle.PickedUp += Retreat;
        }

        private void Retreat()
        {
            _oreToHandle.PickedUp -= Retreat;
            _agent.destination = WorkManager.gameObject.transform.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IPickUpable item) && !_orePicked && WorkManager != null)
            {
                if (item != _oreToHandle)
                {
                    WrongOre((Ore)item);
                }
                _oreToHandle.PickedUp.Invoke();
                _oreToHandle.transform.SetParent(transform);
                _oreToHandle.transform.localPosition = new(0f, 3f, 0f);
                _orePicked = true;
            }

            if (other.gameObject.GetComponent<WorkersManager>() == WorkManager && _oreToHandle != null)
            {
                _oreToHandle.PickedUp -= Retreat;
                _oreToHandle.ReturnToPool();
                _orePicked = false;
                _oreToHandle = null;
                OreDelivered.Invoke();
                WorkManager.Workers.Enqueue(this);
                _agent.destination = WorkManager.BaseSpawnPositions();
            }
            
        }

        public void GoForNewBase(Vector3 newBasePos)
        {
            _agent.destination = newBasePos;
            WorkManager = null;
        }

        private void WrongOre(Ore ore)
        {
            _oreToHandle.Engaged = false;
            _oreToHandle.PickedUp -= Retreat;
            _oreToHandle = ore;
            _oreToHandle.Engaged = true;
            _oreToHandle.PickedUp += Retreat;
        }
    }
}
