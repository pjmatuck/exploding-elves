using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Object
{
    Queue<T> poolQueue;

    public ObjectPool()
    {
        poolQueue = new Queue<T>();
    }

    public bool TryGetObject(out T obj)
    {
        obj = null;

        if (poolQueue.Count == 0)
            return false;
        
        obj = poolQueue.Dequeue();
        return true;
    }

    public void Return(T obj)
    {
        poolQueue.Enqueue(obj);
    }
}
