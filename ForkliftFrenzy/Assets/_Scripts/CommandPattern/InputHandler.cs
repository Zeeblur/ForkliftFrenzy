using UnityEngine;
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

        Command input = handleInput();
        if (input != null)
        {
            Debug.Log("input not null");
            input.Execute(player);
        }
	}

    Command handleInput()
    {
        // get input from axes
        // Raw input has fixed values -1, 0 or 1
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if (vert != 0.0f)
        {
            return new DriveCommand(vert);
        }

        if (horz != 0.0f)
        {
            return new TurnCommand(horz);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            return new RaiseCommand();

        if (Input.GetKeyDown(KeyCode.E))
            return new LowerCommand();

        return null;
    }
}
