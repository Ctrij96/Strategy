using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private float _scanRate;

    private List<Ore> _oreToHandle = new List<Ore>();

    public float OreAmount = 0;
    public Queue<Worker> Workers = new Queue<Worker>();

    private void FindAvaibleOreForFreeWorkers()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 50f);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent<Ore>(out Ore ore))
            {
                if (!ore.Engaged)
                {
                    _oreToHandle.Add(ore);
                    ore.Engaged = true;
                }
                if (_oreToHandle.Count <= Workers.Count)
                    break;
            }
        }
    }

    private void SendWorkers()
    {
        Ore closestOre = _oreToHandle[0];
        foreach (Ore ore in _oreToHandle)
        {
            if (Vector3.Distance(closestOre.transform.position, transform.position) > Vector3.Distance(ore.transform.position, transform.position))
            {
                closestOre = ore;
            }
        }
        if (Workers.Count != 0)
        {
            Workers.Dequeue()
                   .SetOreToPickUp(closestOre);
        }
        _oreToHandle.Remove(closestOre);
    }

    private IEnumerator BaseScanner()
    {
        while (true)
        {
            yield return new WaitForSeconds(_scanRate);
            Debug.Log("В очереди " + Workers.Count.ToString());
            Debug.Log("Руды на разбор " + _oreToHandle.Count.ToString());
            FindAvaibleOreForFreeWorkers();
            if (_oreToHandle.Count != 0)
            {
                SendWorkers();
            }
        }
    }
    private void Start()
    {
        StartCoroutine("BaseScanner");
    }

    private void Update()
    {
        
    }
}
