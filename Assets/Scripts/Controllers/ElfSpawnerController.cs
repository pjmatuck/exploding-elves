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

        SetupObject(spawnedObject, position, Quaternion.identity);
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
        if (Model.DoNotUseObjectPool)
            return InstantiateObject();

        GameObject spawnedObject;
        if (!_pool.TryGetObject(out spawnedObject))
        {
            spawnedObject = InstantiateObject();
        }

        return spawnedObject;
    }

    GameObject InstantiateObject()
    {
        var spawnedObject = Instantiate(_spawningObject);
        spawnedObject.transform.rotation = this.transform.rotation;
        spawnedObject.SetActive(false);
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
                _audioController.PlaySimpleHit();
                break;
            case ElfCollisionType.DifferentColor:
                go.GetComponent<ElfController>().OnCollision -= OnCollisionHandle;
                _audioController.PlayExplosion();
                TakeObjectBack(go);
                gameController.VfxSpawner.SpawnExplosion(collisionPosition);
                break;
        }
    }

    protected void TakeObjectBack(GameObject go)
    {
        if(Model.DoNotUseObjectPool)
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        _pool.Return(go);
    }
}
