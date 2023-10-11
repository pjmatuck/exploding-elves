using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerControlView : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text spawnSpeed;
    [SerializeField] Button increaseSpeed;
    [SerializeField] Button decreaseSpeed;

    public void BindSpeedControl(Action increase, Action decrease)
    {
        increaseSpeed.onClick.AddListener(() =>
        {
            increase.Invoke();
        });
        decreaseSpeed.onClick.AddListener(() =>
        {
            decrease.Invoke();
        });
    }

    public void SetPlayerName(string name)
    {
        playerName.text = name;
    }

    public void SetSpawnSpeed(float speed)
    {
        spawnSpeed.text = $"{speed.ToString("0.0")}/s";
    }
}
