using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Models/Spawner")]
public class SpawnerModel : ScriptableObject
{
    [SerializeField] float spawnTime;
    [SerializeField] bool limitSpawning;
    [SerializeField] int limitSize;

    int spawnerId;
    public int SpawnerId
    {
        get { return spawnerId; }
        set { spawnerId = value; }
    }

    public float SpawnTime
    {
        get { return spawnTime; }
        set { 
            spawnTime = value; 
            OnSpawnTimeChanges(SpawnerId);
        }
    }

    public float SpawnSpeed => 1f / SpawnTime;
    public bool LimitSpawning => limitSpawning;
    public int LimitSize => limitSize;

    public event Action<int> OnSpawnTimeChanges;
}
