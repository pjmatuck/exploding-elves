using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class Players
    {
        public Transform spot;
        public GameObject playerObject;

        [HideInInspector]
        public ElfController ElfController
        {
            get { return playerObject.GetComponent<ElfController>();}
        }
    }

    [SerializeField] List<Players> players;

    List<SpawnerController> spawners;
    List<UIPlayerControlView> viewControllers;

    int playersCount;

    void Start()
    {
        spawners = new List<SpawnerController>();
        viewControllers = new List<UIPlayerControlView>();

        //playersCount = PlayerPrefs.GetInt("PlayersCount");
        playersCount = 4;

        SetupPlayers();

        SetupControls();
    }

    private void SetupControls()
    {
        var uiPlayerControl = Resources.Load("Prefabs/UI/PlayerControl");
        var uiController = ServiceLocator.Instance.Get<UIController>();

        for(int i = 0; i < playersCount; i++)
        {
            var uiPlayerControlGameObject = Instantiate(uiPlayerControl, uiController.ControlsHolder);
            var uiPlayerControlView = uiPlayerControlGameObject.GetComponent<UIPlayerControlView>();
            uiPlayerControlView.BindSpeedControl(spawners[i].IncreaseSpawnSpeed, spawners[i].DecreaseSpawnSpeed);
            uiPlayerControlView.SetPlayerName(players[i].ElfController.Model.Color.ToString());
            uiPlayerControlView.SetSpawnSpeed(spawners[i].Model.SpawnSpeed);
            viewControllers.Add(uiPlayerControlView);
        }
    }

    private void SetupPlayers()
    {
        var spawner = Resources.Load("Prefabs/Spawner/Spawner");

        for(int i = 0; i < playersCount; i++) 
        {
            var spawnerGameObject = Instantiate(spawner, players[i].spot);
            var spawnerController = spawnerGameObject.GetComponent<SpawnerController>();
            spawnerController.InitSpawner(players[i].playerObject, OnSpawnTimeChangesHandler, i);
            spawners.Add(spawnerController);
        }
    }

    void OnSpawnTimeChangesHandler(int spawnerId)
    {
        viewControllers[spawnerId].SetSpawnSpeed(spawners[spawnerId].Model.SpawnSpeed);
    }
}
