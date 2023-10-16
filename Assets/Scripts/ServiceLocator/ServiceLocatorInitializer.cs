using UnityEngine;

public class ServiceLocatorInitializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Debug.Log("[ServiceLocator] Initialize");
        ServiceLocator.Initialize();
    }    
}