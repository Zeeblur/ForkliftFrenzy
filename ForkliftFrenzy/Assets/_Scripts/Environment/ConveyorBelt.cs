using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

    // Assign position to head towards
    public Transform beltEnd;
    // Belt speed
    private float speed = 2.0f;

    FMOD.Studio.EventInstance conveyorSFX;
    
    void Start()
    {
        // Assign event instance to conveyor SFX
        conveyorSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/Conveyor_Belt");
        // Attach it to conveyor rigidbody
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(conveyorSFX, GetComponent<Transform>(), GetComponent<Rigidbody>() );
        // Plays constantly
        //conveyorSFX.start();
    }
    // Moves pick-ups along belt at speed
    void OnTriggerStay(Collider col)
    {
        // added new trigger. Need 


        if (col.isTrigger)
        {
            col.transform.position =  Vector3.MoveTowards(col.transform.position, beltEnd.position, speed * Time.deltaTime);
        }
    }
       


}
