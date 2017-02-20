using UnityEngine;
using System.Collections;

// Zoe Wall 16/01/17
// Abstract command class for character controller commands

public abstract class Command
{
    public abstract void Execute(GameActor actor);
}
