public class IncreaseSpawnSpeedCommand : ICommand
{
    SpawnerModel spawnerModel;

    public IncreaseSpawnSpeedCommand(SpawnerModel model)
    {
        spawnerModel = model;
    }

    public void Execute()
    {
        spawnerModel.SpawnSpeed += .1f;
    }
}
