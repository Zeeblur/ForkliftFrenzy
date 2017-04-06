using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    GameActor player;

    FMOD.Studio.EventInstance forksUpSFX;
    FMOD.Studio.EventInstance forksDownSFX;

    // Use this for initialization
    void Start ()
    {
        player = gameObject.GetComponent<GameActor>();
        // Assign events to instances
        forksUpSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Forklift_Forks/Forks_up");
        forksDownSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Forklift_Forks/Forks_down");
        // Attach to rigidbody
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(forksUpSFX, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(forksDownSFX, GetComponent<Transform>(), GetComponent<Rigidbody>());
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
        if (Input.GetKeyDown(KeyCode.Q))
            forksUpSFX.start();

        if (Input.GetKeyDown(KeyCode.E))
            forksDownSFX.start();

        if(Input.GetKeyUp(KeyCode.Q))
            forksUpSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if(Input.GetKeyUp(KeyCode.E))
            forksDownSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        return commands;
    }
}
