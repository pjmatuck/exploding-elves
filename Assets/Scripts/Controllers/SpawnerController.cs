using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] SpawnerModel model;

    public SpawnerModel Model => model;

    GameObject spawnObject;
    Transform _transform;
    ObjectPool pool;

    int elfCounter = 0;
    float timeToSpawn = 0;
    
    void Start()
    {
        //InvokeRepeating(nameof(SpawnElf), 0f, model.SpawnTime); //TODO: Change this to update control
        _transform = this.transform;
        pool = new ObjectPool();
    }

    void Update()
    {
        timeToSpawn += Time.deltaTime;
        if(timeToSpawn >= model.SpawnTime)
        {
            timeToSpawn = 0;
            SpawnObject(_transform.position);
        }
    }

    public void InitSpawner(GameObject spawnObject, Action<int> OnSpawnTimeChangesHandler, int spawnerId)
    {
        this.spawnObject = spawnObject;
        model.SpawnerId = spawnerId;
        model.OnSpawnTimeChanges += OnSpawnTimeChangesHandler;
    }

    GameObject GetObject()
    {
        GameObject elfGameObject;
        if (!pool.TryGetObject(out elfGameObject))
        {
            elfGameObject = Instantiate(spawnObject);
            elfGameObject.SetActive(false);
        }
        
        return elfGameObject;
    }

    void SpawnObject(Vector3 position)
    {
        if (model.LimitSpawning && elfCounter >= model.LimitSize) return;

        GameObject elfGameObject = GetObject();

        SetupObject(elfGameObject, position, Quaternion.Euler(new Vector3(0,90,0)));
    }

    void SetupObject(GameObject elfGameObject, Vector3 position, Quaternion rotation)
    {
        elfGameObject.SetActive(true);
        elfGameObject.transform.parent = _transform;
        elfGameObject.transform.position = position;
        elfGameObject.transform.rotation = rotation;

        var elfController = elfGameObject.GetComponent<ElfController>();
        elfController.InspectorElfName = $"Elf{elfCounter++}";
        elfController.OnCollision += OnCollisionHandle;
    }

    void OnCollisionHandle(GameObject go, Vector3 otherGameObjectPosition, ElfCollisionType collisionType, bool handlingCollision)
    {
        switch (collisionType)
        {
            case ElfCollisionType.SameColor:
                if(!handlingCollision)
                    SpawnObject(CalculateSpawnPoint(go.transform.position, otherGameObjectPosition));
            break;
            case ElfCollisionType.DifferentColor:
                go.GetComponent<ElfController>().OnCollision -= OnCollisionHandle;
                TakeObjectBack(go);
            break;  
        }
    }

    void TakeObjectBack(GameObject go)
    {
        go.SetActive(false);
        pool.Return(go);
    }

    Vector3 CalculateSpawnPoint(Vector3 position1, Vector3 position2)
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
