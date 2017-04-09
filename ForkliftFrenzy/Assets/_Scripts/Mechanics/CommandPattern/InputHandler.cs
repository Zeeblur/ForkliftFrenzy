using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using FMOD.Studio;
using FMODUnity;

public class InputHandler : MonoBehaviour
{
    GameActor player;

    EventInstance forksUpSFX;
    EventInstance forksDownSFX;
    EventInstance driveUnloaded;
    EventInstance engineIdle;
    

    // Use this for initialization
    void Start ()
    {
        player = gameObject.GetComponent<GameActor>();
        // Assign events to instances
        forksUpSFX = RuntimeManager.CreateInstance("event:/Forklift_Forks/Forks_up");
        forksDownSFX = RuntimeManager.CreateInstance("event:/Forklift_Forks/Forks_down");
        driveUnloaded = RuntimeManager.CreateInstance("event:/Forklift_1_Redneck/Engine_Full");
        engineIdle = RuntimeManager.CreateInstance("event:/Forklift_1_Redneck/Engine_Full");
        // Attach to this rigidbody
        RuntimeManager.AttachInstanceToGameObject(forksUpSFX, GetComponent<Transform>(), GetComponent<Rigidbody>());
        RuntimeManager.AttachInstanceToGameObject(forksDownSFX, GetComponent<Transform>(), GetComponent<Rigidbody>());
        RuntimeManager.AttachInstanceToGameObject(driveUnloaded, GetComponent<Transform>(), GetComponent<Rigidbody>());

        // Setup params as necesasry
        forksUpSFX.setVolume(2.0f);
        forksDownSFX.setVolume(2.0f);
        forksUpSFX.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        forksDownSFX.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        driveUnloaded.setParameterValue("RPM", 2000);
        driveUnloaded.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        engineIdle.setParameterValue("RPM", 1000);
        engineIdle.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        engineIdle.start();
    }
	
	// Update is called once per frame
	void Update ()
    {

        // multi input support
        List<Command> input = handleInput();
        if (input != null)
        {
            foreach (Command c in input)
                c.Execute(player);
        }
	}

    List<Command> handleInput()
    {
        List<Command> commands = new List<Command>();

        // get input from axes
        // Raw input has fixed values -1, 0 or 1
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        bool move = false;


        if (vert != 0.0f)
        {
            commands.Add(new DriveCommand(vert));
            move = true;
        }

        // always send turn command for resetting wheels if no input
        commands.Add(new TurnCommand(horz, move));


        if (Input.GetKey(KeyCode.Q))
            commands.Add(new RaiseCommand());

        if (Input.GetKey(KeyCode.E))
            commands.Add(new LowerCommand());

        // SFX
        // Raise forks
        if (Input.GetKeyDown(KeyCode.Q))
            forksUpSFX.start();
        // Lower forks
        if (Input.GetKeyDown(KeyCode.E))
            forksDownSFX.start();
        // Stop raise 
        if (Input.GetKeyUp(KeyCode.Q))
            forksUpSFX.stop(STOP_MODE.ALLOWFADEOUT);
        // Stop lower
        if (Input.GetKeyUp(KeyCode.E))
            forksDownSFX.stop(STOP_MODE.ALLOWFADEOUT);
        // Start drive, stop idle
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            engineIdle.stop(STOP_MODE.ALLOWFADEOUT);
            driveUnloaded.start();
        }
        // Stop drive, start idle
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            engineIdle.start();
            driveUnloaded.stop(STOP_MODE.ALLOWFADEOUT);
        }


        return commands;
    }
}
