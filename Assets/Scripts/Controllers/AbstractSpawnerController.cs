using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpawnerController : MonoBehaviour
{
    [SerializeField] SpawnerModel model;
    public SpawnerModel Model => model;

    protected int spawnedObjectsCounter = 0;
    protected Transform _transform;
    protected AudioController _audioController;

    float elapsedSpawnTime = 0;
    protected abstract void InitPool();
    protected abstract void SpawnObject(Vector3 position);
    public abstract void SetSpawningObject(UnityEngine.Object obj);
    
    void Start()
    {
        _transform = this.transform;
        _audioController = ServiceLocator.Instance.Get<AudioController>();
        InitPool();
    }

    void Update()
    {
        if (model.SpawnOnDemand) return;
        
        elapsedSpawnTime += Time.deltaTime;
        if (elapsedSpawnTime >= model.SpawnTime)
        {
            elapsedSpawnTime = 0;
            SpawnObject(_transform.position);
        } 
    }

    protected Vector3 CalculateSpawnPoint(Vector3 position1, Vector3 position2)
    {
        Vector3 newPosition = new Vector3
        {
            x = Mathf.Abs((position1.x - position2.x)/2f),
            y = 0f,
            z = Mathf.Abs((position1.z - position2.z)/2f),
        };
        return position1 + newPosition;
    }

    public void IncreaseSpawnSpeed()
    {
        CommandManager.ExecuteCommand(
            new IncreaseSpawnSpeedCommand(model));
    }

    public void DecreaseSpawnSpeed()
    {
        CommandManager.ExecuteCommand(
            new DecreaseSpawnSpeedCommand(model));
    }
}
