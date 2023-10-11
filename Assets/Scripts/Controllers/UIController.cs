using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour, IGameService
{
    [SerializeField] RectTransform controlsHolder;

    public RectTransform ControlsHolder => controlsHolder;

    private void Awake()
    {
        ServiceLocator.Instance.Register(this);
    }
}