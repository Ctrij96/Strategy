using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private GameObject _orePrefab;
    [SerializeField] private float _spawnRate;
    [SerializeField] private Transform _container;

    private Plane _field;

    private Vector3 RandomPos()
    {
        Vector3 pos = _field.ClosestPointOnPlane(Vector3.zero + _spawnRange * Random.onUnitSphere) + Vector3.up * _spawnHeight;
        return pos;
    }

    private void SpawnOre()
    {
        Instantiate(_orePrefab, RandomPos(), Quaternion.identity, _container);
    }

    private void Start()
    {
        _field.normal = Vector3.up;
        InvokeRepeating("SpawnOre", 3f, _spawnRate);
    }
}
