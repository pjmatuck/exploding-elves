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
    [SerializeField] bool doNotUseObjectPool;

    int spawnerId;
    public int SpawnerId
    {
        get { return spawnerId; }
        set { spawnerId = value; }
    }
    public float SpawnTime => SpawnSpeed > 0 ? 1f / SpawnSpeed : float.MaxValue;
    public float SpawnSpeed
    {
        get { return spawnSpeed; }
        set
        {
            if (value < 0f)
                value = 0f;
            spawnSpeed = value;
            OnSpawnSpeedChanges(SpawnerId);
        }
    }
    public bool LimitSpawning => limitInstances;
    public int LimitSize => limitInstancesCount;
    public bool SpawnOnDemand => spawnOnDemand;
    public bool DoNotUseObjectPool => doNotUseObjectPool;

    public event Action<int> OnSpawnSpeedChanges;
}
