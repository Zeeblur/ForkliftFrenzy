using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    GameActor player;

	// Use this for initialization
	void Start ()
    {
        player = gameObject.GetComponent<GameActor>();
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

        return commands;
    }
}
