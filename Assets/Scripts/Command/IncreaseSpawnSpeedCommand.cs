using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpawnSpeedCommand : ICommand
{
    SpawnerModel spawnerModel;

    public IncreaseSpawnSpeedCommand(SpawnerModel model)
    {
        spawnerModel = model;
    }

    public void Execute()
    {
        spawnerModel.SpawnTime -= .1f;
    }
}
