using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : IGameService
{
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
    }
}
