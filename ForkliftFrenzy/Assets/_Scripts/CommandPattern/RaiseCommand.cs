using UnityEngine;
using System.Collections;

// Zoe Wall 17/01/17
// Concrete implementation of raise command to raise forklift

public class RaiseCommand : Command
{
    public override void Execute(GameActor actor)
    {
        actor.RaiseForks();
    }
}