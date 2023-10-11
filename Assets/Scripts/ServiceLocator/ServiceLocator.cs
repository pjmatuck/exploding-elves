using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private ServiceLocator() { }

    private readonly Dictionary<string, IGameService> services = 
        new Dictionary<string, IGameService>();

    public static ServiceLocator Instance { get ; private set; }

    public static void Initialize()
    {
        Instance = new ServiceLocator();
    }

    public T Get<T>() where T : IGameService
    {
        string key = typeof(T).Name;
        if(!services.ContainsKey(key))
        {
            Debug.LogError($"{key} not registered.");
            throw new System.InvalidOperationException();
        }
        return (T)services[key];
    }

    public void Register<T>(T service) where T : IGameService
    {
        string key = typeof(T).Name;
        if (services.ContainsKey(key))
        {
            Debug.LogError($"{key} is already registered.");
            return;
        }
        services.Add(key, service);

    }

    public void Unregister<T>() where T : IGameService
    {
        string key = typeof(T).Name;
        if (!services.ContainsKey(key))
        {
            Debug.LogError($"{key} is not registered.");
            return;
        }
        services.Remove(key);
    }
}
