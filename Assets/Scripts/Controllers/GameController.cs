using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour, IGameService
{
    [Serializable]
    public class Players
    {
        public Transform SceneSpot;
        public ElfController Elf;
        public AbstractSpawnerController Spawner;
    }

    [SerializeField] List<Players> players;
    [SerializeField] VFXSpawnerController vfxController;
    [SerializeField] ParticleSystem explosionEffect;
    public VFXSpawnerController VfxSpawner => vfxController;

    List<UIPlayerControlView> viewControllers;

    int playersCount;

    void Start()
    {
        ServiceLocator.Instance.Register(this);

        viewControllers = new List<UIPlayerControlView>();

        //playersCount = PlayerPrefs.GetInt("PlayersCount");
        playersCount = 4;

        SetupPlayers();

        SetupControls();

        SetupVFX();
    }

    private void SetupControls()
    {
        var uiPlayerControl = Resources.Load("Prefabs/UI/PlayerControl");
        var uiController = ServiceLocator.Instance.Get<UIController>();

        for(int i = 0; i < playersCount; i++)
        {
            var spawner = players[i].Spawner;
            var uiPlayerControlGameObject = Instantiate(uiPlayerControl, uiController.ControlsHolder);
            var uiPlayerControlView = uiPlayerControlGameObject.GetComponent<UIPlayerControlView>();
            uiPlayerControlView.BindSpeedControl(spawner.IncreaseSpawnSpeed, spawner.DecreaseSpawnSpeed);
            uiPlayerControlView.SetPlayerName(players[i].Elf.Model.Color.ToString());
            uiPlayerControlView.SetSpawnSpeed(spawner.Model.SpawnSpeed);
            viewControllers.Add(uiPlayerControlView);
        }
    }

    private void SetupPlayers()
    {
        for(int i = 0; i < playersCount; i++) 
        {
            var spawnerController = (ElfSpawnerController)Instantiate(players[i].Spawner, players[i].SceneSpot);
            spawnerController.SetSpawningObject(players[i].Elf.gameObject);
            spawnerController.SetupListeners(OnSpawnTimeChangesHandler, i);
        }
    }

    void SetupVFX()
    {
        vfxController = Instantiate(vfxController, this.transform);
        vfxController.SetSpawningObject(explosionEffect);
    }

    void OnSpawnTimeChangesHandler(int spawnerId)
    {
        viewControllers[spawnerId].SetSpawnSpeed(players[spawnerId].Spawner.Model.SpawnSpeed);
    }
}
