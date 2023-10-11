using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecreaseSpawnSpeedCommand : ICommand
{
    SpawnerModel spawnerModel;

    public DecreaseSpawnSpeedCommand(SpawnerModel model)
    {
        spawnerModel = model;
    }

    public void Execute()
    {
        spawnerModel.SpawnTime += .1f;
    }
}
