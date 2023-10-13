using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Models/Spawner")]
public class SpawnerModel : ScriptableObject
{
    [SerializeField] float spawnSpeed;
    [SerializeField] bool limitInstances;
    [SerializeField] int limitInstancesCount;
    [SerializeField] bool spawnOnDemand;

    int spawnerId;
    public int SpawnerId
    {
        get { return spawnerId; }
        set { spawnerId = value; }
    }
    public float SpawnTime => 1f / SpawnSpeed;
    public float SpawnSpeed
    {
        get { return spawnSpeed; }
        set
        {
            spawnSpeed = value;
            OnSpawnSpeedChanges(SpawnerId);
        }
    }
    public bool LimitSpawning => limitInstances;
    public int LimitSize => limitInstancesCount;
    public bool SpawnOnDemand => spawnOnDemand;

    public event Action<int> OnSpawnSpeedChanges;
}
