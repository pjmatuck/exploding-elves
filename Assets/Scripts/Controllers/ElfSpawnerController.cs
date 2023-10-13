using System;
using UnityEngine;

public class ElfSpawnerController : AbstractSpawnerController
{
    GameController gameController;
    ObjectPool<GameObject> _pool;
    GameObject _spawningObject;

    protected override void InitPool()
    {
        _pool = new ObjectPool<GameObject>();
    }
    public override void SetSpawningObject(UnityEngine.Object obj)
    {
        _spawningObject = (GameObject)obj;
    }

    protected override void SpawnObject(Vector3 position)
    {
        if (Model.LimitSpawning && spawnedObjectsCounter >= Model.LimitSize) return;

        GameObject spawnedObject = GetObject();

        SetupObject(spawnedObject, position, Quaternion.Euler(new Vector3(0, 90, 0)));
    }

    private void SetupObject(GameObject spawnedObject, Vector3 position, Quaternion rotation)
    {
        spawnedObject.SetActive(true);
        spawnedObject.transform.parent = _transform;
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;

        var elfController = spawnedObject.GetComponent<ElfController>();
        elfController.OnCollision += OnCollisionHandle;
    }

    public void SetupListeners(Action<int> OnSpawnTimeChangesHandler, int spawnerId)
    {
        Model.SpawnerId = spawnerId;
        Model.OnSpawnSpeedChanges += OnSpawnTimeChangesHandler;
    }

    protected GameObject GetObject()
    {
        GameObject spawnedObject;
        if (!_pool.TryGetObject(out spawnedObject))
        {
            spawnedObject = Instantiate(_spawningObject);
            spawnedObject.SetActive(false);
        }

        return spawnedObject;
    }

    void OnCollisionHandle(GameObject go, Vector3 otherGameObjectPosition, ElfCollisionType collisionType, bool handlingCollision)
    {
        var collisionPosition = base.CalculateSpawnPoint(go.transform.position, otherGameObjectPosition);
        gameController = ServiceLocator.Instance.Get<GameController>();

        switch (collisionType)
        {
            case ElfCollisionType.SameColor:
                if (!handlingCollision)
                    SpawnObject(collisionPosition);
                break;
            case ElfCollisionType.DifferentColor:
                go.GetComponent<ElfController>().OnCollision -= OnCollisionHandle;
                TakeObjectBack(go);
                gameController.VfxSpawner.SpawnExplosion(collisionPosition);
                break;
        }
    }

    protected void TakeObjectBack(GameObject go)
    {
        go.SetActive(false);
        _pool.Return(go);
    }
}