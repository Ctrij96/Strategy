using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private BaseManager _baseOwner;
    private Ore _oreToHandle;
    private Vector3 _destination;

    public void SetOreToPickUp(Ore ore)
    {
        _oreToHandle = ore;
        _destination = _oreToHandle.transform.position;
    }

    private void Update()
    {
        if (_oreToHandle != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
            transform.forward = (transform.position - _destination).normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ore>(out Ore ore))
        {
            if (ore == _oreToHandle)
            {
                _destination = _baseOwner.transform.position;
                _oreToHandle.transform.SetParent(transform, true);
            }
        }

        if (collision.gameObject.TryGetComponent<BaseManager>(out BaseManager basement))
        {
            if (basement == _baseOwner)
            {
                {
                    Destroy(_oreToHandle.gameObject);
                    _oreToHandle = null;
                    _baseOwner.OreAmount += 1;
                    _baseOwner.Workers.Enqueue(this);
                }
            }
        }
    }

    private void Start()
    {
        _baseOwner.Workers.Enqueue(this);
    }
}
