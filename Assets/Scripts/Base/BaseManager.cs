using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Base
{
    [RequireComponent(typeof(WorkersManager))]
    [RequireComponent(typeof(OreScanner))]
    public class BaseManager : MonoBehaviour
    {
        public bool Constructing = false;

        private WorkersManager _workersManager;
        private OreScanner _oreScanner;
        private int _oreCount = 0;
        private Vector3 _newBasePos;
            
            private void Awake()
        {
            _workersManager = GetComponent<WorkersManager>();
            _oreScanner = GetComponent<OreScanner>();
        }

        private void Update()
        {
            CheckOreCount();
            CheckOreForWorker();
        }

        private void CheckOreCount()
        {
            if (_oreCount >= 3 && !Constructing)
            {
                AddWorker();
                _oreCount = 0;
            }

            if (_oreCount >= 5 && Constructing)
            {
                if (_workersManager.Workers.Count > 0)
                {
                    _workersManager.SendWorkerToNewBase(_newBasePos);
                    Constructing = false;
                    _oreCount = 0;
                }
            }
            
        }

        public void AddWorker()
        {
            Worker worker = _workersManager.NewWorker();
            worker.OreDelivered += OreCountUp;
        }

        private void CheckOreForWorker()
        {
            var closestOre = _oreScanner.ClosestOre();
            if (closestOre != null && _workersManager.Workers.Count > 0)
            {
                closestOre.Engaged = true;
                _workersManager.SendWorkerForOre(closestOre);
            }
        }

        private void OreCountUp()
        {
            _oreCount += 1;
        }

        public void ConstructNewBase(Vector3 newBasePos)
        {
            Constructing = true;
            _newBasePos = newBasePos;
        }
    }
}