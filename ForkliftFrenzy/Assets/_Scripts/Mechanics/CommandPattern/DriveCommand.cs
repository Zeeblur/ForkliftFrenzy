using UnityEngine;
using System.Collections;

// Zoe Wall 16/01/17
// Concrete implementation of drive command to move char forward

public class DriveCommand : Command
{
    private float driveValue;

    public DriveCommand(float input)
    {
        driveValue = input;
    }

    public override void Execute(GameActor actor)
    {
        actor.Drive(driveValue);
    }
}
