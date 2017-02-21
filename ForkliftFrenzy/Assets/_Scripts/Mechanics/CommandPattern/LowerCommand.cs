using UnityEngine;
using System.Collections;

// Zoe Wall 17/01/17
// Concrete implementation of lower command to move forklift

public class LowerCommand : Command
{
    public override void Execute(GameActor actor)
    {
        actor.LowerForks();
    }
}