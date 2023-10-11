using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Queue<GameObject> poolQueue;

    public ObjectPool()
    {
        poolQueue = new Queue<GameObject>();
    }

    public bool TryGetObject(out GameObject go)
    {
        go = null;

        if (poolQueue.Count == 0)
            return false;
        
        go = poolQueue.Dequeue();
        return true;
    }

    public void Return(GameObject go)
    {
        poolQueue.Enqueue(go);
    }
}
