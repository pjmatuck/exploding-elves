public class DecreaseSpawnSpeedCommand : ICommand
{
    SpawnerModel spawnerModel;

    public DecreaseSpawnSpeedCommand(SpawnerModel model)
    {
        spawnerModel = model;
    }

    public void Execute()
    {
        spawnerModel.SpawnSpeed -= .1f;
    }
}
